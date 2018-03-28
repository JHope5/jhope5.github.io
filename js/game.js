// keycodes for movement
var keys = {
	// Arrow keys
	37: 'left',
	38: 'up',
	39: 'right',
	40: 'down',
	// WASD
	65: 'aleft',
	87: 'wup',
	68: 'dright',
	83: 'sdown'
};

var levelFiles = [
	'levelOne.js',
	'levelTwo.js',
	'levelThree.js',
	'levelFour.js',
	'levelFive.js'
];

var display;
var msg;
var editor;
var map;

var currentLevel = 0;

function init() {
	display = new ROT.Display({width: dimensions.width, height: dimensions.height, fontSize: 15, fontStyle: "bold"});

	// drawObject finds the symbol and colour of an object
	display.drawObject = function (x, y, object, bgColour, multiplicand) {
		var symbol = objects[object].symbol;
		var colour;
		if (objects[object].colour) {
			colour = objects[object].colour;
		} else {
			colour = "#fff";
		}

		if (!bgColour) {
			bgColour = "#000";
		}

		if (multiplicand) {
			colour = ROT.Color.toHex(ROT.Color.multiply(multiplicand, ROT.Color.fromString(colour)));
			bgColour = ROT.Color.toHex(ROT.Color.multiply(multiplicand, ROT.Color.fromString(bgColour)));
		}

		display.draw(x, y, symbol, colour, bgColour);
	};

	display.drawAll = function(map, multiplicand) {
		for (var x = 0; x < dimensions.width; x++) {
			for (var y = 0; y < dimensions.height; y++) {
				this.drawObject(x, y, map._grid[x][y].type, map._grid[x][y].bgColour, multiplicand);
			}
		}
		if (map.player) { map.player.draw(); }

	}

	$('#screen').append(display.getContainer());

	// required so all canvas elements can detect keyboard events
	$("canvas").first().attr("contentEditable", "true");
	display.getContainer().addEventListener("keydown", function(e) {
		if (keys[e.keyCode]) {
			map.player.move(keys[e.keyCode]);
		}
	});
	display.getContainer().addEventListener("click", function(e) {
		$(display.getContainer()).addClass('focus');
		$('.CodeMirror').removeClass('focus');
	});

	msg = new ROT.Display({width: dimensions.width, height: 2, fontSize: 15});
	$('#msg').append(msg.getContainer());
	msg.write = function(text) {
		msg.clear();
		msg.drawText(0, 0, text);
	}

	map = new Map(display);
	getLevel(currentLevel);
}

function moveToNextLevel() {
	currentLevel++;
	getLevel(currentLevel);
};

// AJAX request to get the level text file and load it
function getLevel(levelNumber) {
	var fileName;
	if (levelNumber < levelFiles.length) {
		fileName = levelFiles[levelNumber];
	}
	else {
		fileName = "thereIsNoLevel.js";
	}
	$.get('levels/' + fileName, function (codeText) {
		if (editor) {
			editor.toTextArea();
		}
		loadLevel(codeText, levelNumber);
	});
}

function loadLevel(lvlCode, lvlNum) {
	// initialize CodeMirror editor
	editor = CodeMirror.fromTextArea(document.getElementById("editor"), {
		tabMode: 'indent',
		lineNumbers: true,
		dragDrop: false,
		theme: 'eclipse',
		extraKeys: {'Enter': function () {}}
	});
	editor.setSize(600, 500);
	editor.on("focus", function(instance) {
		$('.CodeMirror').addClass('focus');
		$('#screen canvas').removeClass('focus');
	});

	// initialize level
	editor.setValue(lvlCode);

	// get editable line ranges from level metadata
	levelMetadata = editor.getLine(0);
	editableLineRanges = JSON.parse(levelMetadata.slice(3)).editable;
	editableLines = [];
	for (var j = 0; j < editableLineRanges.length; j++) {
		range = editableLineRanges[j];
		for (var i = range[0]; i <= range[1]; i++) {
			editableLines.push(i - 1);
		}
	}
	editor.removeLine(0);

	// only allow editing on certain lines
	// don't allow removal of lines
	// set line length limit of 80 characters
	editor.on('beforeChange', function (instance, change) {
		if (editableLines.indexOf(change.to.line) == -1 || change.to.line != change.from.line || (change.to.ch > 80 && change.to.ch >= change.from.ch)) {
				change.cancel();
		}
	});

	// set background colour for lines that can't be changed
	editor.on('update', function (instance) {
		for (var i = 0; i < editor.lineCount(); i++) {
			if (editableLines.indexOf(i) == -1) {
				instance.addLineClass(i, 'wrap', 'disabled');
			}
		}
	});
	editor.refresh();

	// editor.getPlayerCode returns only the code written in editable lines
	editor.getPlayerCode = function () {
		var code = '';
		for (var i = 0; i < editor.lineCount(); i++) {
			if (editableLines.indexOf(i) > -1) {
				code += editor.getLine(i) + ' \n';
			}
		}
		return code;
	}
	submitCode(lvlNum);
}

function resetEditor() {
    getLevel(currentLevel);
}

function submitCode(lvlNum) {
	var executedCode = editor.getValue();
	var playerCode = editor.getPlayerCode();
	var validatedStartLevel = validate(executedCode, playerCode, currentLevel);
	if (validatedStartLevel) {
		map.reset();
		validatedStartLevel(map);
		if (lvlNum >= levelFiles.length) {
			// don't do this for thereIsNoLevel.js
			return;
		}
		display.drawAll(map);
	}
}