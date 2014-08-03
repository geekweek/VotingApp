(function (database) {

    var mongodb = require("mongodb");
    var mongoUrl = "mongodb://localhost:27017/TestDb";
    var theDb = null;

    database.getDb = function (next) {
        if (!theDb) {
            //create db connection
            mongodb.MongoClient.connect(mongoUrl, function (err, db) {
                if (err) {
                    next(err, null);
                } else {
                    theDb = {
                        db: db,
                        notes: db.collection("notes"),
                        users: db.collection("users"),
                        topics:db.collection("topics")
                    };
                    next(null, theDb);
                }
            });
        } else {
            next(null, theDb);
        }
    };


})(module.exports);