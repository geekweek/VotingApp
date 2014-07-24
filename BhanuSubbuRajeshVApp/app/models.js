var mongoose = require("mongoose");
Schema = mongoose.Schema;

var TeamSchema = new Schema({
	name : String,
	captain : String
});
mongoose.model("Team",TeamSchema);