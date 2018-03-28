// {"editable": [[16, 32]]}
/*
 * Level 4
 *
 * Nice one! That was another easy one. 
 * Let's change things a little bit and see how 
 * you do when you can't just get rid of all the walls,
 * and add in a LOT more for you to deal with.
 * 
 * The function at the end makes sure there are a 
 * certain amount of walls on the map, and only one goal.
 * 
 * Good luck!
 */
function startLevel(map) {
    map.placePlayer(map.getWidth()-10, map.getHeight()-3);

    for (y = 5; y <= map.getHeight(); y++) {
        map.placeObject(5, y, 'wall');
        map.placeObject(15, y, 'wall');
        map.placeObject(25, y, 'wall');
        map.placeObject(35, y, 'wall');
        map.placeObject(map.getWidth() - 5, y, 'wall');
    }

    for (x = 5; x <= map.getWidth() - 5; x++) {
        map.placeObject(x, 20, 'wall');
        map.placeObject(x, 15, 'wall');
        map.placeObject(x, 10, 'wall');
        map.placeObject(x, 5, 'wall');
        map.placeObject(x, map.getHeight() - 1, 'wall');
    }

    map.placeObject(7, 7, 'goal');
}

function validateLevel(map) {
    numWalls = 5 * (map.getHeight()-5) + 5 * (map.getWidth()-10);
    validateAtLeastXObjects(map, numWalls, 'wall');
    validateExactlyXObjects(map, 1, 'goal');
}