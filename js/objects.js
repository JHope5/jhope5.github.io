var objects = {
	'empty' : {
		'symbol': ' ',
		'passable': true
	},
	'wall': {
		'symbol': '#',
		'colour': '#b30000',
		'passable': false
	},
	'goal' : {
		'symbol' : 'O',
		'colour': '#0f0',
		'passable': true,
		'onCollision': function (player) {
			// player moves onto goal
			loadNextLevel();
		}
	},
	'player' : {
		'symbol': 'P',
		'colour': '#fff',
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