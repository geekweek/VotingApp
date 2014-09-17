var express = require("express");
var app = express();

var db = require("mongoose").connect("mongodb://localhost/Football");
require("./app/models");

require("./app/routes")(app);
app.use(express.static(__dirname + "/public"));

var port = 8080;
app.listen(port);
console.log("We're all set");