var map, layersControl, overlaysControl, allVars;
function init() {
    L.mapbox.accessToken = 'pk.eyJ1IjoiZ2F2aW5zdG9rZXMiLCJhIjoiZDRhZmZlMDI1OWUzZTk0NTBlZDE3ZmJkMDkzMmRkNTcifQ.tOPvTsGoEYR6fNsgxAbj1w';
    map = new L.mapbox.map('map', 'mapbox.streets')
        .setView([53.2884650585551, -6.37335353008102], 7);
    markers = new L.MarkerClusterGroup({ chunkedLoading: true });
}



function search(searchName, searchCounty, searchDateStart, searchDateEnd) {
    markers.clearLayers();

    if (searchCounty === "") {
        searchCounty = 0;
    }



    $.ajax({
        url: "../compuwreck/Api/Shipwreck?" + 'searchName=' + searchName + '&county=' + searchCounty + '&dateStart=' + searchDateStart + '&dateEnd=' + searchDateEnd,
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {

            for (var i = 0; i < data.length; i++) {
                var title = data[i].ShipwreckName;
                var shipwreckId = data[i].ShipwreckId;
                var marker = L.marker(new L.LatLng(data[i].Ltd, data[i].Lng), {
                    icon: L.mapbox.marker.icon({ 'marker-symbol': 'marker', 'marker-color': '0044FF' }),
                    title: title
                });

                var link = '<div class="mapbutton"><a href="../compuwreck/Shipwreck/Details/' + shipwreckId + '"  >VIEW</a></div>';
                var lng = data[i].Lng;
                var Ltd = data[i].Ltd;

                marker.bindPopup("<h3>" + title + "</h3>" + "<br />" + "<strong>LNG: </strong>" + lng + "<br />" + "<strong>LTD: </strong>" + Ltd + "<br />" + link);
                markers.addLayer(marker);
            }

            map.addLayer(markers);
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('error - ' + textStatus);
        }
    });
}


