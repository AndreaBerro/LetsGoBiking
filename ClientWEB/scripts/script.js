const API_URI = "http://localhost:8733/Design_Time_Addresses/Routing/RoutingService/rest";

let map;
let lastLayer = 0;
let pathLayers = [null,null,null];

function initMap() {
    map = new ol.Map({
        target: 'map', // <-- This is the id of the div in which the map will be built.
        layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM()
            })
        ],
    
        view: new ol.View({
            center: ol.proj.fromLonLat([5.400, 43.300]), // <-- Those are the GPS coordinates to center the map to.
            zoom: 14 // You can adjust the default zoom.
        })
    });

    let stations = get_stations();
    stations.then(stations => {
        draw_stations(stations, map);
    });
}

// Get the stations from the API
async function get_stations() {
    let response = await fetch(API_URI + "/getStations");
    let responseJson = await response.json();
    let stations = JSON.parse(responseJson);
    return stations;
}

// Draw the stations on the map
function draw_stations(stations, map) {
    const stations_source = new ol.source.Vector({
		features: stations.map(station => {
			return new ol.Feature({
				geometry: new ol.geom.Point(ol.proj.fromLonLat([station.position.longitude, station.position.latitude])),
				name: station.name
			})
		})
	});

	map.addLayer(new ol.layer.Vector({
		source: stations_source,
		style: new ol.style.Style({
			image: new ol.style.Icon({
				anchor: [0.7, 0.7],
				scale: [0.023, 0.023],
				anchorXUnits: 'fraction',
				anchorYUnits: 'fraction',
				src: `ClientWEB/data/bike.png`
			})
		})
	}));
}

// Get the path between two stations
async function get_path(from, to) {
    let response = await fetch(API_URI + "/getPath?location1=" + from + "&location2=" + to);
    let responseJson = await response.json();
    let path = JSON.parse(responseJson);
    return path;
}

// Draw the path between the points
function draw_path(paths, map) {

    let i = 0;
    //if the path is layer already on the map, remove it
    while (i<lastLayer) {
        map.removeLayer(pathLayers[i]);
        i++;
    }

    let color = "#cc6600";
    i = 0;
    while (i < paths.length) {
        //if it's a foot path we choose a different color than the bike path
        if (i%2 == 0) {
            color = "#cc6600";
        }
        else {
            color = "#1751e6";
        }

        // we draw the (foot or bike) path
        let path = paths[i];

        var lineString = new ol.geom.LineString(path.map(point => {
            return ol.proj.fromLonLat([point.longitude, point.latitude]);
        }));
    
        var lineFeature = new ol.Feature({
            geometry: lineString,
            name: 'Line'
        });
        
        var lineStyle = new ol.style.Style({
            stroke: new ol.style.Stroke({
                color: color,
                width: 4
            })
        });
    
        var source = new ol.source.Vector({
            features: [lineFeature]
        });
    
        var vectorLayer = new ol.layer.Vector({
            source: source,
            style: lineStyle
        });
    
        map.addLayer(vectorLayer);
        pathLayers[i] = vectorLayer;
        i++;
    }
    lastLayer = i;
}

// search the path between the start and the destination
function searchPath() {
    let from = document.getElementById("from").value;
    let to = document.getElementById("to").value;
    let path = get_path(from, to);
    path.then(path => {
        draw_path(path, map);
    });
}

// Set the width of the side navigation to 250px and the left margin of the page content to 250px 
function openNav() {
    document.getElementById("mySidenav").style.width = "450px";
    document.getElementById("openSidenav").style.display = "none";
}

// Set the width of the side navigation to 0 and the left margin of the page content to 0 
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("openSidenav").style.display = "block";
}


// --- Initialization ---
initMap();