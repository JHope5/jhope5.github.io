
var forbidden = ['eval', 'prototype', 'delete', 'return', 'moveToNextLevel', 'document'];

var validationRulesByLevel = [ null ];

var DummyDisplay = function () {
	this.clear = function () {};
	this.draw = function () {};
	this.drawObject = function () {};
};

function validate(allCode, playerCode, level) {
	validateLevel = function () {};

	msg.clear();
	try {
		for (var i = 0; i < forbidden.length; i++) {
			var forbiddenWord = forbidden[i];
			if (playerCode.indexOf(forbiddenWord) > -1) {
				throw forbiddenWord + ' is forbidden!';
			}
		}

		var dummyMap = new Map(new DummyDisplay);

		eval(allCode);

		startLevel(dummyMap);
		if (typeof(validateLevel) != 'undefined') {
			validateLevel(dummyMap);
		}

		return startLevel;
	} catch (e) {
		msg.drawText(0, 0, e.toString());
	}
}

function validateAtLeastXObjects(map, num, type) {
	var count = 0;
	for (var x = 0; x < map.getWidth(); x++) {
		for (var y = 0; y < map.getHeight(); y++) {
			if (map._grid[x][y].type === type) {
				count++;
			}
		}
	}
	if (count < num) {
		throw 'You need to have at least ' + num + ' ' + type + 's on the map! You only have ' + count + '.';
	}
}

function validateExactlyXObjects(map, num, type) {
	var count = 0;
	for (var x = 0; x < map.getWidth(); x++) {
		for (var y = 0; y < map.getHeight(); y++) {
			if (map._grid[x][y].type === type) {
				count++;
			}
		}
	}
	if (count != num) {
		throw 'You need to have EXACTLY ' + num + ' ' + type + 's on the map! You have ' + count + '.';
	}
}