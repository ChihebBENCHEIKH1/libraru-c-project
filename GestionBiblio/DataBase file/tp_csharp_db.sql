-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le :  ven. 08 fév. 2019 à 19:08
-- Version du serveur :  5.7.19
-- Version de PHP :  7.1.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `tp_csharp_db`
--

-- --------------------------------------------------------

--
-- Structure de la table `cds`
--

DROP TABLE IF EXISTS `cds`;
CREATE TABLE IF NOT EXISTS `cds` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `auteur` varchar(255) DEFAULT NULL,
  `titre` varchar(255) DEFAULT NULL,
  `num_ouvrage` varchar(255) DEFAULT NULL,
  `date_emprunt` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `cds`
--

INSERT INTO `cds` (`id`, `auteur`, `titre`, `num_ouvrage`, `date_emprunt`) VALUES
(1, 'CDA2', 'CDT2', '2666', 'dimanche 13 janvier 2019'),
(3, 'CDT3', 'CDA3', '2777', 'dimanche 13 janvier 2019'),
(4, 'CDA4', 'CDT4', '2888', 'dimanche 13 janvier 2019');

-- --------------------------------------------------------

--
-- Structure de la table `emprunteurs`
--

DROP TABLE IF EXISTS `emprunteurs`;
CREATE TABLE IF NOT EXISTS `emprunteurs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `client` varchar(255) DEFAULT NULL,
  `cin` varchar(255) DEFAULT NULL,
  `date_emprunt` varchar(255) DEFAULT NULL,
  `delai` varchar(255) DEFAULT NULL,
  `num_ouvrage` int(11) NOT NULL,
  `type_ouvrage` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `emprunteurs`
--

INSERT INTO `emprunteurs` (`id`, `client`, `cin`, `date_emprunt`, `delai`, `num_ouvrage`, `type_ouvrage`) VALUES
(7, 'AKI', '00033', '08-02-2019 18:29:29', '10-02-2019', 3, 'periodiques');

-- --------------------------------------------------------

--
-- Structure de la table `livres`
--

DROP TABLE IF EXISTS `livres`;
CREATE TABLE IF NOT EXISTS `livres` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `auteur` varchar(255) DEFAULT NULL,
  `titre` varchar(255) DEFAULT NULL,
  `editeur` varchar(255) DEFAULT NULL,
  `num_ouvrage` varchar(255) DEFAULT NULL,
  `date_emprunt` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `livres`
--

INSERT INTO `livres` (`id`, `auteur`, `titre`, `editeur`, `num_ouvrage`, `date_emprunt`) VALUES
(9, 'livreA', 'livreT', 'livreE', '1555', 'dimanche 13 janvier 2019');

-- --------------------------------------------------------

--
-- Structure de la table `periodiques`
--

DROP TABLE IF EXISTS `periodiques`;
CREATE TABLE IF NOT EXISTS `periodiques` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(255) DEFAULT NULL,
  `periodicite` varchar(255) DEFAULT NULL,
  `numero` varchar(255) DEFAULT NULL,
  `num_ouvrage` varchar(255) DEFAULT NULL,
  `date_emprunt` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `periodiques`
--

INSERT INTO `periodiques` (`id`, `nom`, `periodicite`, `numero`, `num_ouvrage`, `date_emprunt`) VALUES
(1, 'Nom2', 'PP', 'NN111', '3555', 'dimanche 13 janvier 2019'),
(2, 'Nom1', 'Per1', 'NN152', '3777', 'dimanche 13 janvier 2019'),
(3, 'Nom3', 'Per3', 'NN245', '3555', 'lundi 14 janvier 2019'),
(5, 'TestEMSI', 'chaque semaine', '5566', '2019', 'mercredi 16 janvier 2019');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
