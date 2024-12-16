
//var map = L.map('map', {
//    center: [38.65533, 35.466287],  // Türkiye 
//    zoom: 6,                         
//    zoomSnap: 0.5                    //yarim zoom
//});

//// Harita üzerine tile layer ekleme
//var basemap = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
//    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
//}).addTo(map);

//// Harita üzerinde 6.5 zoom seviyesine ayarla
//map.setZoom(6.5);

//// Harita üzerine marker eklemek için
//var mark = map.on('click', function (e) {
//    var marker = L.marker(e.latlng).addTo(map)
//        .bindPopup('Buraya işaret koydunuz!').openPopup();
//});

//// Çizim kontrolü ekleme
//var drawnItems = new L.FeatureGroup();
//map.addLayer(drawnItems);

//var drawControl = new L.Control.Draw({
//    edit: {
//        featureGroup: drawnItems
//    },
//    draw: {
//        polygon: {
//            allowIntersection: false, // Çokgenlerin kesişmesini engelle
//            showArea: true // Alanı göster
//        },
//        polyline: false, // Çizgi çizimi devre dışı
//        circle: false, // Daire çizimi devre dışı
//        rectangle: true, // Dikdörtgen çizimi aktif
//        marker: false, // Marker ekleme devre dışı
//    }
//});
//map.addControl(drawControl);

//map.on(L.Draw.Event.CREATED, function (event) {
//    var layer = event.layer;
//    drawnItems.addLayer(layer);
//});

//// Evler üzerinde seçim yapılması için
//drawnItems.on('click', function (e) {
//    e.layer.bindPopup('Bu evi seçtiniz!').openPopup();
//});


var map = L.map('map', {
    center: [38.3552, 38.3095],  // Malatya merkezi
    zoom: 7,                   // Daha yakın bir zoom seviyesi
    zoomSnap: 0.5
});

// Harita üzerine tile layer ekleme
var basemap = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

// SABİT MARKER: Malatya'da bir konum
var fixedCoords = [38.3552, 38.3095]; // Malatya merkezi koordinatları
var fixedMarker = L.marker(fixedCoords).addTo(map)
    .bindPopup("Malatya'da sabit bir nokta!").openPopup();

//// Kullanıcı tarafından haritaya tıklanarak marker ekleme
//map.on('click', function (e) {
//    var marker = L.marker(e.latlng).addTo(map)
//        .bindPopup('Buraya işaret koydunuz!').openPopup();
//});
