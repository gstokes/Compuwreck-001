var map, layersControl, overlaysControl, allVars;
function init() {
    L.mapbox.accessToken = 'pk.eyJ1IjoiZ2F2aW5zdG9rZXMiLCJhIjoiZDRhZmZlMDI1OWUzZTk0NTBlZDE3ZmJkMDkzMmRkNTcifQ.tOPvTsGoEYR6fNsgxAbj1w';
    map = new L.mapbox.map('map', 'mapbox.streets')
        .setView([53.2884650585551, -6.37335353008102], 7);
    markers = new L.MarkerClusterGroup({ chunkedLoading: true });
}


function doAjaxCall() {
    $.ajax({
        url: "http://localhost:62208/Api/Shipwreck?" + 'searchName=test&'+'county=0',
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {

            for (var i = 0; i < data.length; i++) {
                var title = data[i].ShipwreckName;
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


function search(searchName, searchCounty) {
    markers.clearLayers();

    if (searchName === "") {
        searchName = "none";
    }

    if (searchCounty === "") {
        searchCounty = 0;
    }
    console.log(searchName);
    console.log(searchCounty);

    $.ajax({
        url: "http://localhost:62208/Api/Shipwreck?" + 'searchName=' + searchName + '&county=' + searchCounty,
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {

            for (var i = 0; i < data.length; i++) {
                var title = data[i].ShipwreckName;
                var marker = L.marker(new L.LatLng(data[i].Ltd, data[i].Lng), {
                    icon: L.mapbox.marker.icon({ 'marker-symbol': 'marker', 'marker-color': '0044FF' }),
                    title: title
                });

                marker.bindPopup(title);
                markers.addLayer(marker);
            }

            map.addLayer(markers);
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('error - ' + textStatus);
        }
    });
}


