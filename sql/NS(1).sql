-- Create the table if it doesn't exist
CREATE TABLE IF NOT EXISTS netherlands_train_stations (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    hasLift BOOLEAN NOT NULL,
    wheelChairAccessible BOOLEAN NOT NULL,
    hasToilet BOOLEAN NOT NULL,
    hasKiosk BOOLEAN NOT NULL
);

-- Insert data for Dutch train stations
INSERT INTO netherlands_train_stations (name, hasLift, wheelChairAccessible, hasToilet, hasKiosk) VALUES
-- Amsterdam stations
('Amsterdam Centraal', TRUE, TRUE, TRUE, TRUE),
('Amsterdam Sloterdijk', TRUE, TRUE, TRUE, TRUE),
('Amsterdam Zuid', TRUE, TRUE, TRUE, TRUE),
('Amsterdam Amstel', TRUE, TRUE, TRUE, TRUE),
('Amsterdam Bijlmer ArenA', TRUE, TRUE, TRUE, TRUE),
('Amsterdam Science Park', FALSE, TRUE, FALSE, FALSE),
('Amsterdam Muiderpoort', FALSE, TRUE, FALSE, FALSE),

-- Rotterdam stations
('Rotterdam Centraal', TRUE, TRUE, TRUE, TRUE),
('Rotterdam Alexander', TRUE, TRUE, TRUE, FALSE),
('Rotterdam Blaak', TRUE, TRUE, TRUE, TRUE),
('Rotterdam Lombardijen', TRUE, TRUE, FALSE, FALSE),

-- Utrecht stations
('Utrecht Centraal', TRUE, TRUE, TRUE, TRUE),
('Utrecht Zuilen', FALSE, TRUE, FALSE, FALSE),
('Utrecht Overvecht', FALSE, TRUE, FALSE, FALSE),
('Utrecht Lunetten', FALSE, TRUE, FALSE, FALSE),

-- The Hague (Den Haag) stations
('Den Haag Centraal', TRUE, TRUE, TRUE, TRUE),
('Den Haag HS', TRUE, TRUE, TRUE, TRUE),
('Den Haag Laan van NOI', TRUE, TRUE, TRUE, FALSE),
('Den Haag Mariahoeve', FALSE, TRUE, FALSE, FALSE),
('Den Haag Ypenburg', FALSE, TRUE, FALSE, FALSE),

-- Groningen stations
('Groningen', TRUE, TRUE, TRUE, TRUE),
('Groningen Europapark', TRUE, TRUE, TRUE, FALSE),
('Groningen Noord', FALSE, TRUE, FALSE, FALSE),

-- Hoorn stations
('Hoorn', TRUE, TRUE, TRUE, TRUE),
('Hoorn Kersenboogerd', TRUE, TRUE, FALSE, FALSE),

-- Arnhem stations
('Arnhem Centraal', TRUE, TRUE, TRUE, TRUE),
('Arnhem Presikhaaf', FALSE, TRUE, FALSE, FALSE),
('Arnhem Velperpoort', FALSE, TRUE, FALSE, FALSE),
('Arnhem Zuid', FALSE, TRUE, FALSE, FALSE),

-- Amersfoort stations
('Amersfoort Centraal', TRUE, TRUE, TRUE, TRUE),
('Amersfoort Schothorst', TRUE, TRUE, FALSE, FALSE),
('Amersfoort Vathorst', TRUE, TRUE, FALSE, FALSE),

-- Leiden stations
('Leiden Centraal', TRUE, TRUE, TRUE, TRUE),
('De Vink (Leiden)', FALSE, TRUE, FALSE, FALSE),

-- Other large cities
('Eindhoven Centraal', TRUE, TRUE, TRUE, TRUE),
('Eindhoven Strijp-S', TRUE, TRUE, TRUE, FALSE),
('Haarlem', TRUE, TRUE, TRUE, TRUE),
('Haarlem Spaarnwoude', FALSE, TRUE, FALSE, FALSE),

-- Zwolle and Breda
('Zwolle', TRUE, TRUE, TRUE, TRUE),
('Breda', TRUE, TRUE, TRUE, TRUE),

-- Smaller city additions
('Tilburg', TRUE, TRUE, TRUE, TRUE),
('Tilburg Reeshof', TRUE, TRUE, FALSE, FALSE),
('Alkmaar', TRUE, TRUE, TRUE, TRUE),
('Alkmaar Noord', FALSE, TRUE, FALSE, FALSE),

-- Other stations
('Maastricht', TRUE, TRUE, TRUE, TRUE),
('Heerlen', TRUE, TRUE, TRUE, TRUE),
('Hengelo', TRUE, TRUE, TRUE, TRUE),
('Deventer', TRUE, TRUE, TRUE, TRUE),
('Assen', TRUE, TRUE, TRUE, TRUE),
('Vlissingen', TRUE, TRUE, TRUE, TRUE),
('Roermond', TRUE, TRUE, TRUE, TRUE),
('Oss', FALSE, TRUE, FALSE, TRUE),
('Hilversum', TRUE, TRUE, TRUE, TRUE),
('Zaandam', TRUE, TRUE, TRUE, TRUE),
('Ede-Wageningen', TRUE, TRUE, TRUE, TRUE),
('Enschede', TRUE, TRUE, TRUE, TRUE),
('Almere Centrum', TRUE, TRUE, TRUE, TRUE),
('Lelystad Centrum', TRUE, TRUE, TRUE, TRUE),
('Culemborg', FALSE, TRUE, FALSE, FALSE),
('Veenendaal Centrum', FALSE, TRUE, FALSE, FALSE),
('Barendrecht', TRUE, TRUE, FALSE, FALSE),
('Gouda', TRUE, TRUE, TRUE, TRUE),
('Schiphol Airport', TRUE, TRUE, TRUE, TRUE),
('Zaandijk Zaanse Schans', FALSE, TRUE, FALSE, FALSE),
('Hoofddorp', TRUE, TRUE, TRUE, TRUE),
('Houten', TRUE, TRUE, TRUE, FALSE),
('Weesp', TRUE, TRUE, TRUE, TRUE),
('Dordrecht', TRUE, TRUE, TRUE, TRUE),
('Rijswijk', TRUE, TRUE, TRUE, FALSE),
('Vlaardingen Centrum', FALSE, TRUE, FALSE, TRUE),
('Capelle Schollevaar', FALSE, TRUE, FALSE, FALSE),
('Harderwijk', TRUE, TRUE, TRUE, FALSE),
('Heerenveen', TRUE, TRUE, TRUE, TRUE),
('Meppel', TRUE, TRUE, TRUE, TRUE),
('Kampen Zuid', FALSE, TRUE, FALSE, FALSE),
('Bussum Zuid', TRUE, TRUE, FALSE, FALSE),
('Zandvoort aan Zee', FALSE, TRUE, FALSE, TRUE),
('Den Helder', TRUE, TRUE, TRUE, FALSE);

