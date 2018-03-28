var objects = {
	'empty' : {
		'symbol': ' ',
		'passable': true
	},
	'wall': {
		'symbol': '#',
		'colour': '#fff',
		'passable': false
	},
	'goal' : {
		'symbol' : 'O',
		'colour': '#0f0',
		'passable': true,
		'onCollision': function (player) {
			// player moves onto goal
			moveToNextLevel();
		}
	},
	'player' : {
		'symbol': 'P',
		'colour': '#0ff',
		'passable': false
	},
	'mine' : {
		'symbol': ' ',
		'passable': true,
		'onCollision': function (player) {
			player.hitMine();
		}
	}
};