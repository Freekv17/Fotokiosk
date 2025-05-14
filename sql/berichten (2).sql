-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Gegenereerd op: 14 mei 2025 om 08:53
-- Serverversie: 8.0.30
-- PHP-versie: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `nsdatabase`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `berichten`
--

CREATE TABLE `berichten` (
  `id` int NOT NULL,
  `naam` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'Anoniem',
  `kleinBericht` varchar(100) NOT NULL,
  `grootBericht` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `gekeurd` varchar(255) NOT NULL DEFAULT 'nietGekeurd',
  `station` varchar(255) NOT NULL,
  `gemaakt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Gegevens worden geëxporteerd voor tabel `berichten`
--

INSERT INTO `berichten` (`id`, `naam`, `kleinBericht`, `grootBericht`, `gekeurd`, `station`, `gemaakt`) VALUES
(1, 'Anoniem', 'Dit is een pracht station', 'Het station is beetje te krap maar overal gewoon goed', 'nietGekeurd', 'den helder', '2025-05-14 10:43:38'),
(2, 'Swen', 'test', 'test', 'nietGekeurd', 'den helder', '2025-05-14 10:43:38'),
(3, 'Piet', 'tets', 'test', 'nietGekeurd', 'Meppel', '2025-05-14 10:43:38'),
(4, '', '', '', 'nietGekeurd', 'Meppel', '2025-05-14 10:43:38'),
(5, 'roei', 'Kut station', 'meeeui', 'nietGekeurd', 'Den Helder', '2025-05-14 10:43:38'),
(6, 'Swen', 'kut station', 'raar apart station geen uitzicht', 'nietGekeurd', 'Meppel', '2025-05-14 10:43:38'),
(7, '', 'test', 'test', 'nietGekeurd', 'Meppel', '2025-05-14 10:43:38'),
(8, 'Anoniem', 'test', 'test', 'nietGekeurd', 'Meppel', '2025-05-14 10:43:38'),
(9, 'test', 'test', 'test', 'nietGekeurd', 'Den Helder', '2025-05-08 10:44:02'),
(10, 'test`', 's', 's', 'nietGekeurd', 'Den Helder', '2025-05-14 10:47:18');

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `berichten`
--
ALTER TABLE `berichten`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `berichten`
--
ALTER TABLE `berichten`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
