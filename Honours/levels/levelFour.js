// {"editable": [[17, 31]]}
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
    map.placePlayer(map.getWidth()-10, map.getHeight()-4);

    for (y = 4; y <= map.getHeight() - 2; y++) {
        map.placeObject(5, y, 'wall');
        map.placeObject(15, y, 'wall');
        map.placeObject(25, y, 'wall');
        map.placeObject(35, y, 'wall');
        map.placeObject(map.getWidth() - 5, y, 'wall');
    }

    for (x = 5; x <= map.getWidth() - 5; x++) {
        map.placeObject(x, 18, 'wall');
        map.placeObject(x, 13, 'wall');
        map.placeObject(x, 8, 'wall');
        map.placeObject(x, 3, 'wall');
        map.placeObject(x, map.getHeight() - 2, 'wall');
    }

    map.placeObject(9, 5, 'goal');
}

function validateLevel(map) {
    numWalls = 3 * (map.getHeight()-10) + 6 * (map.getWidth()-10);
    validateAtLeastXObjects(map, numWalls, 'wall');
    validateExactlyXObjects(map, 1, 'goal');
}