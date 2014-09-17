var express = require("express");
var router = express.Router();
var teams = require("./controllers");

module.exports = function(app){
	app.post("/team/add/:name/:captain",teams.create);
};