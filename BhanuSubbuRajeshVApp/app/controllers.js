var mongoose = require("mongoose");
var Team = mongoose.model("Team");

exports.create = function(req,res){
	var team = new Team({"name":req.params["name"],"captain":req.params["captain"]});

	team.save(function(err) {
		if (err) {
			return res.send(400, {
				message: "Error creating team"
			});
		} else {
			res.jsonp(team);
		}
	});
};