db = db.getSiblingDB("CareApiDB");

db.createCollection("Pacient");
db.createCollection("Therapy");
db.createCollection("User");

db.Pacient.insertMany([
  {
    Name: "Joana",
    BirthDate: "1980-05-21",
    Disease: "Esclerose Múltipla",
    Sessions: [
      {
        StartedAt: "2023-11-15T09:30:00",
        EndedAt: "2023-11-15T10:30:00",
        TherapyName: "Terapia Ocupacional",
        Results: "Melhoria da coordenação motora",
      },
      {
        StartedAt: "2023-11-17T11:00:00",
        EndedAt: "2023-11-17T12:00:00",
        TherapyName: "Hidroterapia",
        Results: "Aumento da força muscular",
      },
    ],
  },
]);

db.Therapy.insertMany([
  {
    Name: "Terapia cognitiva",
    CreatedByUser: "Maria",
    Command: [
      {
        Name: "início",
        Codigo: "inicio",
        ImageUrl: "http://example.com/image1.jpg",
        SoundUrl: "http://example.com/sound1.mp3",
      },
      {
        Name: "se",
        Codigo: "se",
        ImageUrl: "http://example.com/image2.jpg",
        SoundUrl: "http://example.com/sound2.mp3",
      },
      {
        Name: "tocar",
        Codigo: "tocar()",
        ImageUrl: "http://example.com/image3.jpg",
        SoundUrl: "http://example.com/sound3.mp3",
      },
    ],
  },
]);

db.User.insertMany([
  {
    "Name": "João",
    "Email": "joaosilva@example.com",
    "Role": "Admin",
    "Password": "senha123"
  },

  {
    "Name": "Maria",
    "Email": "mariasouza@example.com",
    "Role": "User",
    "Password": "12345678"
  },

  {
    "Name": "Carlos",
    "Email": "carlospereira@example.com",
    "Role": "Manager",
    "Password": "senhaSegura123"
  },

]);
