var mongoose = require('mongoose'),
    Schema = mongoose.Schema,
    ObjectId = Schema.ObjectId;

var membershipSchema = new Schema({
    provider:  String,
    name:String,
    email:String,
    userId: {type: ObjectId, ref: 'User'},
    dateAdded: {type: Date, default: Date.now}
});

mongoose.model('Membership', membershipSchema);