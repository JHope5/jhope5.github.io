// {"editable": [[32, 32]]}
/*
 * Level 5
 *
 * Look how nice and easy this one is, just get to the goal
 * and you're finished. I should probably mention that 
 * there are mines scattered all over the place. I'd call 
 * this level 'Minesweeper' but that name's already taken... 
 * 
 * There might be a way to reveal where the mines are. 
 * Try not to blow up.
 */
function randomNum (min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function startLevel(map) {
    for (x = 0; x < map.getWidth(); x++) {
        for (y = 0; y < map.getHeight(); y++) {
            map.setSquareColour(x, y, '#000');
        }
    }

    map.placePlayer(map.getWidth() - 5, 5);

    for (var i = 0; i < 50; i++) {
        var x = randomNum(0, map.getWidth() - 1);
        var y = randomNum(0, map.getHeight() - 1);
        if (x != map.getWidth() - 1 && y != map.getHeight() - 1) {
            // don't place mine over goal!
            map.placeObject(x, y, 'mine', '#000');
        }
    
    }
    map.placeObject(2, map.getHeight() - 1, 'goal');
}

function validateLevel(map) {
    validateAtLeastXObjects(map, 45, 'mine');
    validateExactlyXObjects(map, 1, 'goal');
}