db = db.getSiblingDB("CareApiDB");

db.createCollection("Pacient");
db.createCollection("Therapy");
db.createCollection("User");

db.User.insertMany([
  {
    _id: "0cda05e3-d0f2-4ab5-8551-972ae59bdb20",
    Name: "Giovana",
    Email: "glisboathome@gmail.com",
    Role: "admin",
    Password: "zqYvznhRLrAcMdle8BN21qGRyCuDBo3Cg/8WfYCMa7g=",
  },
]);