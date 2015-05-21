var map, layersControl, overlaysControl, allVars;
function init() {
    $.blockUI({ overlayCSS: { backgroundColor: 'black' } });
    var googleLayerROADMAP = new L.Google('ROADMAP');
    var googleLayerHYBRID = new L.Google('HYBRID');
    var googleLayerTERRAIN = new L.Google('TERRAIN');

    var tiles = L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
        maxZoom: 14,
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors, Points &copy 2012 LINZ'
    }),
	latlng = L.latLng(53.2884650585551, -6.37335353008102);
    map = new L.Map('map', {
        center: new L.LatLng(53.2884650585551, -6.37335353008102),
        zoom: 7,
        layers: [tiles]
    });


    var baseMaps = {
        "Roadmap": tiles
    };

    layersControl = new L.control.layers(baseMaps, null, { collapsed: false }).addTo(map);
    doAjaxCall();
    $.unblockUI();
}

function doAjaxCall() {
    $.ajax({
        url: "http://localhost:62208/Api/Shipwreck",
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {

            var markers = new L.MarkerClusterGroup({ chunkedLoading: true });
            //console.log(data);
            for (var i = 0; i < data.length; i++) {
                    var title = data[i].ShipwreckName;
                    var marker = L.marker([data[i].Ltd, data[i].Lng], { title: title });

                    marker.bindPopup(title);
                    markers.addLayer(marker);
            }

            map.addLayer(markers);


            //markers.on('clusterclick', function (a) {
            //    alert('cluster ' + a.layer.getAllChildMarkers().length);
            //});
            //markers.on('click', function (a) {
            //    alert('marker ' + a.layer);
            //});
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('error - ' + textStatus);
        }
    });
}

//function mapSuccessHandler(response) {
//    var mapProperties = (typeof response) == 'string' ? eval('(' + response + ')') : response;
//    for (var i = 0; i < mapProperties.length; i++) {
//        if (mapProperties[i].East > 0 && mapProperties[i].North > 0) {
//            L.marker([mapProperties[i].Longitude, mapProperties[i].Latitude]).addTo(map).bindPopup("I am a green leaf.");
//        }
//    }
//}




