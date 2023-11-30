db = db.getSiblingDB("CareApiDB");

db.createCollection("Pacient");
db.createCollection("Therapy");
db.createCollection("User");

db.Pacient.insertMany([
  {
    Name: "Joana",
    BirthDate: "1980-05-21",
    Disease: "Esclerose Múltipla",
    Cif: "D004",
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
    Name: "Maria",
    BirthDate: "1985-04-23",
    Desease: "Artrite Reumatoide",
    Cif: "A001",
    Sessions: [
      {
        StartedAt: "2023-11-11T09:00:00",
        EndedAt: "2023-11-11T10:00:00",
        TherapyName: "Fisioterapia",
        Results: "Melhora da mobilidade",
      },
    ],
  },
  {
    Name: "Roberto",
    BirthDate: "1972-08-15",
    Desease: "Lombalgia",
    Cif: "B002",
    Sessions: [
      {
        StartedAt: "2023-11-12T11:00:00",
        EndedAt: "2023-11-12T12:00:00",
        TherapyName: "Terapia Manual",
        Results: "Redução da dor",
      },
    ],
  },
  {
    Name: "Lucas",
    BirthDate: "1990-01-05",
    Desease: "Tendinite",
    Cif: "C003",
    Sessions: [
      {
        StartedAt: "2023-11-13T14:30:00",
        EndedAt: "2023-11-13T15:30:00",
        TherapyName: "Ultra-som Terapêutico",
        Results: "Diminuição da inflamação",
      },
    ],
  },
]);
