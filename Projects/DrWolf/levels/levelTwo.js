// {"editable": [[13, 31]]}
/*
 * Level 2
 *
 * Good work, but that was the first level for a reason.
 * 
 * No random mazes this time. It's just a shame there are walls
 * in the way... If only you had a hammer or some other way to 
 * get rid of them.
 */
function startLevel(map) {
    map.placePlayer(7, 5);
    
    for (y = 3; y <= map.getHeight() - 10; y++) {
        map.placeObject(5, y, 'wall');
        map.placeObject(map.getWidth() - 5, y, 'wall');
    }

    for (y = 9; y <= map.getHeight() - 13; y++) {
        map.placeObject(19, y, 'wall');
        map.placeObject(map.getWidth() - 27, y, 'wall');
    }
    
    for (x = 5; x <= map.getWidth() - 5; x++) {
        map.placeObject(x, 3, 'wall');
        map.placeObject(x, map.getHeight() - 9, 'wall');
    }

    for (x = 19; x <= map.getWidth() - 27; x++) {
        map.placeObject(x, 9, 'wall');
        map.placeObject(x, map.getHeight() - 12, 'wall');
    }
    
    map.placeObject(map.getWidth() - 29, map.getHeight() - 14, 'goal');
}