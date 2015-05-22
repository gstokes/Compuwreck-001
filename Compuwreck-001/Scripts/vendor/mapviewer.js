var map, layersControl, overlaysControl, allVars;
function init() {
    //$.blockUI({ overlayCSS: { backgroundColor: 'black' } });

    //var tiles = L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
    //    maxZoom: 14,
    //    attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors, Points &copy 2012 LINZ'
    //});

    //map = new L.Map('map', {
    //    center: new L.LatLng(53.2884650585551, -6.37335353008102),
    //    zoom: 7,
    //    layers: [tiles]
    //});

    //layersControl = new L.control.layers(null, { collapsed: false }).addTo(map);
    doAjaxCall();
    //$.unblockUI();
}



function doAjaxCall() {
    $.ajax({
        url: "../compuwreck/Api/Shipwreck",
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {


            L.mapbox.accessToken = 'pk.eyJ1IjoiZ2F2aW5zdG9rZXMiLCJhIjoiZDRhZmZlMDI1OWUzZTk0NTBlZDE3ZmJkMDkzMmRkNTcifQ.tOPvTsGoEYR6fNsgxAbj1w';
            var map = L.mapbox.map('map', 'mapbox.streets')
                .setView([53.2884650585551, -6.37335353008102], 7);

            var markers = new L.MarkerClusterGroup({ chunkedLoading: true });


            //console.log(data);
            for (var i = 0; i < data.length; i++) {
                var title = data[i].ShipwreckName;
                var marker = L.marker([data[i].Ltd, data[i].Lng], { title: title });

                var marker = L.marker(new L.LatLng(data[i].Ltd, data[i].Lng), {
                    icon: L.mapbox.marker.icon({ 'marker-symbol': 'marker', 'marker-color': '0044FF' }),
                    title: title
                });

                marker.bindPopup(title);
                markers.addLayer(marker);
            }

            map.addLayer(markers);
            L.control.scale().addTo(map);
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('error - ' + textStatus);
        }
    });
}





//function doAjaxCall() {
//    $.ajax({
//        url: "http://localhost:62208/Api/Shipwreck",
//        type: 'GET',
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json',
//        success: function (data) {

//            var markers = new L.MarkerClusterGroup({ chunkedLoading: true });
//            //console.log(data);
//            for (var i = 0; i < data.length; i++) {
//                    var title = data[i].ShipwreckName;
//                    var marker = L.marker([data[i].Ltd, data[i].Lng], { title: title });

//                    marker.bindPopup(title);
//                    markers.addLayer(marker);
//            }

//            map.addLayer(markers);
//            L.control.scale().addTo(map);
//        },

//        error: function (XMLHttpRequest, textStatus, errorThrown) {
//            alert('error - ' + textStatus);
//        }
//    });
//}


