var express = require("express");
var app = express();
var passport = require('passport')
  , GoogleStrategy = require('passport-google').Strategy;

var db = require("mongoose").connect("mongodb://localhost/Football");
require("./app/models");
var Membership = require("./app/Membership");
require("./app/routes")(app);
app.use(express.static(__dirname + "/public"));

// Passport session setup.
//   To support persistent login sessions, Passport needs to be able to
//   serialize users into and deserialize users out of the session.  Typically,
//   this will be as simple as storing the user ID when serializing, and finding
//   the user by ID when deserializing.  However, since this example does not
//   have a database of user records, the complete Google profile is serialized
//   and deserialized.
passport.serializeUser(function(user, done) {
  done(null, user);
});

passport.deserializeUser(function(obj, done) {
  done(null, obj);
});

// Use the GoogleStrategy within Passport.
//   Strategies in passport require a `validate` function, which accept
//   credentials (in this case, an OpenID identifier and profile), and invoke a
//   callback with a user object.
passport.use(new GoogleStrategy({
    returnURL: 'http://localhost:8080/auth/google/return',
    realm: 'http://localhost:8080/'
  },
  function(identifier, profile, done) {
    //db.Membership.findOne({
      //      'userId': profile.id 
        //}, function(err, user) {
        //    if (err) {
        //        return done(err);
        //    }
            //No user was found... so create a new user with values from Facebook (all the profile. stuff)
        //    if (!user) {
               // user = new Membership({
                //	userId: profile.id,
               //     name: profile.displayName,
               //     email: profile.emails[0].value,
               //     provider: 'Google',
              //  });
              //  user.save(function(err) {
              //      if (err) console.log(err);
                    return done(null, profile);
             //   });
         //   } else {
                //found user. Return
         //       return done(err, user);
         //   }
        //});
  }
));




// Redirect the user to Google for authentication.  When complete, Google
// will redirect the user back to the application at
//     /auth/google/return
app.get('/auth/google', passport.authenticate('google'));

// Google will redirect the user to this URL after authentication.  Finish
// the process by verifying the assertion.  If valid, the user will be
// logged in.  Otherwise, authentication has failed.
app.get('/auth/google/return', 
  passport.authenticate('google', { successRedirect: '/example',
                                    failureRedirect: '/login' }));


var port = 8080;
app.listen(port);
console.log("We're all set");