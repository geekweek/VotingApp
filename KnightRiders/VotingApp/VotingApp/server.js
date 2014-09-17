var http = require('http');
var express = require("express");
var expressSession = require("express-session");
var bodyParser = require("body-parser");
var cookieParser = require("cookie-parser");
var fs = require("fs");
var busboy = require("connect-busboy");
var app = express();

var controllers = require("./controllers");
var flash = require("connect-flash");

//set the view engine
app.set("view engine", "vash");

//opt into services
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use(cookieParser());
app.use(expressSession({ secret: "VotingApp", 
                        saveUninitialized: true,
                        resave: true
                        }));
app.use(flash());
app.use(busboy()); 

//set the public static resources
app.use(express.static(__dirname + "/public"));

// use authentication
var auth = require("./auth");
auth.init(app);

app.post('/fileupload', function (req, res) {
    var fstream;
    req.pipe(req.busboy);
    req.busboy.on('file', function (fieldname, file, filename) {
        console.log("Uploading: " + filename);
        fstream = fs.createWriteStream(__dirname + '/files/' + filename);
        file.pipe(fstream);
        fstream.on('close', function () {
            res.redirect('back');
        });
    });
});

app.get('/download/:fileName', function (req, res) {
    var fileName = req.params.fileName;
    var file = __dirname + '/files/' + fileName;
    res.download(file); // Set disposition and send it.
});

// map the routes
controllers.init(app);

var server = http.createServer(app);



server.listen(3000);