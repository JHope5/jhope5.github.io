var Player = function(x, y, map) {
	this._x = x;
	this._y = y;
	this._rep = "P";
	this._fgColour = "#fff";
	this._display = map._display;
	this.draw();
}

Player.prototype.draw = function () {
	var bgColour = map._grid[this._x][this._y].bgColour
	this._display.draw(this._x, this._y, this._rep, this._fgColour, bgColour);
}

Player.prototype.atLocation = function (x, y) {
	return (this._x === x && this._y === y);
}

Player.prototype.move = function (direction) {
	var cur_x = this._x;
	var cur_y = this._y;
	var new_x;
	var new_y;

	if ((direction === 'up') || (direction === 'wup')) {
		new_x = cur_x;
		new_y = cur_y - 1;
	}
	else if ((direction === 'down') || (direction === 'sdown')) {
		new_x = cur_x;
		new_y = cur_y + 1;
	}
	else if ((direction === 'left') || (direction === 'aleft')) {
		new_x = cur_x - 1;
		new_y = cur_y;
	}
	else if ((direction === 'right') || (direction === 'dright')) {
		new_x = cur_x + 1;
		new_y = cur_y;
	}

	if (map.canMoveTo(new_x, new_y)) {
		this._display.drawObject(cur_x,cur_y, map._grid[cur_x][cur_y].type, map._grid[cur_x][cur_y].bgColour);
		this._x = new_x;
		this._y = new_y;
		this.draw();
		if (objects[map._grid[new_x][new_y].type].onCollision) {
			objects[map._grid[new_x][new_y].type].onCollision(this);
		}
	}
	else {
		console.log("Can't move to " + new_x + ", " + new_y + ", reported from inside Player.move() method");
	}
}

Player.prototype.hitMine = function () {
	var gameOver = Math.floor((Math.random() * 3) + 1);
	if (gameOver === 1) {
		alert('You stood on a mine! Try not to do that next time.');
		getLevel(currentLevel);
	}
	if (gameOver === 2) {
		alert("BOOM! You're dead. Try again.");
		getLevel(currentLevel);
	}
	if (gameOver === 3) {
		alert("Didn't I say there were mines lying around? Have another go.");
		getLevel(currentLevel);
	}
};