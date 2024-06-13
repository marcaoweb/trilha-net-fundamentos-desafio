
-- Table: Recibos
DROP TABLE IF EXISTS "Recibos";

CREATE TABLE "Recibos" (
    "ReciboId"     INTEGER  PRIMARY KEY
                          NOT NULL,
    "VeiculoId" "int" NULL,
    "HoraEntrada"  "datetime" NOT NULL,
    "HoraSaida"    "datetime",
    "PrecoFixo"    "money",
    "PrecoPorHora" "money",
    "Status"       "bit"      NOT NULL,
    "Total"        "money",
    CONSTRAINT "FK_Recibos_Veiculos" FOREIGN KEY 
	(
		"VeiculoId"
	) REFERENCES "Veiculos" (
		"VeiculoId"
	)
);


-- Table: Veiculos
DROP TABLE IF EXISTS "Veiculos";

CREATE TABLE "Veiculos" (
    "VeiculoId" INTEGER     PRIMARY KEY NOT NULL,
    "Placa"     nvarchar (7) NOT NULL
);
