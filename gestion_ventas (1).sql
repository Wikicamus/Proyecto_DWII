-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost
-- Tiempo de generación: 20-06-2025 a las 00:17:02
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `gestion_ventas`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Client`
--

CREATE TABLE `Client` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `address` varchar(255) NOT NULL,
  `phone` int(12) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `Client`
--

INSERT INTO `Client` (`id`, `name`, `last_name`, `address`, `phone`, `email`, `password`) VALUES
(1, 'Jesus', 'Rojas', 'Calle 8C #13-113', 300612344, 'Jesus@gmail.com', '$2a$11$610QDSC9eLDeLqHYd136cOjGmYzPB94MISu87epCPuC02W54iXAmC'),
(2, 'Jesus', 'Rojas', '', 0, 'user@gmail.com', 'jare2329p'),
(3, 'Jare', 'string', '', 0, 'user2@gmail.com', 'user1234'),
(4, 'ejemplo', 'string', '', 0, 'user@example.com', '$2a$11$CLo9yfP2JrLbHY3SduK94ueLHvDCaT9k8hkdIlvksbH91czMajGZa'),
(5, 'ejemplo', 'string', 'string', 312321, 'us@example.com', '$2a$11$o62GPIk67qxrhm11uN4IWe75Ivt1gbjLM02PXZo1hRYPdjbIwPWty');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Complaint`
--

CREATE TABLE `Complaint` (
  `id` int(10) UNSIGNED NOT NULL,
  `idsale` int(10) UNSIGNED NOT NULL,
  `reason` varchar(255) NOT NULL,
  `description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `DeliveryStatus`
--

CREATE TABLE `DeliveryStatus` (
  `id` int(10) UNSIGNED NOT NULL,
  `idsale` int(10) UNSIGNED NOT NULL,
  `description` varchar(255) NOT NULL,
  `state` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `DeliveryStatus`
--

INSERT INTO `DeliveryStatus` (`id`, `idsale`, `description`, `state`) VALUES
(1, 3, 'string', 'string'),
(2, 3, 'El producto esta siendo recogido por el transportista', 'En bodega');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Employee`
--

CREATE TABLE `Employee` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `Employee`
--

INSERT INTO `Employee` (`id`, `name`, `last_name`, `email`, `password`) VALUES
(1, 'NombreEmpleado', 'ApellidoEmpleado', 'empleado@empresa.com', '$2a$11$8YhPIXE1kYbhltPnZWO7NeSfYv97Gk2VdB9.ACxQAeN6kCI7BM83m');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Inventory`
--

CREATE TABLE `Inventory` (
  `id` int(10) UNSIGNED NOT NULL,
  `id_product` int(10) UNSIGNED NOT NULL,
  `id_employee` int(10) UNSIGNED NOT NULL,
  `movement_type` enum('IN','OUT') NOT NULL DEFAULT 'IN',
  `quantity` int(11) NOT NULL DEFAULT 1,
  `movement_date` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `Inventory`
--

INSERT INTO `Inventory` (`id`, `id_product`, `id_employee`, `movement_type`, `quantity`, `movement_date`) VALUES
(7, 3, 2, 'IN', 16, '2025-06-18 02:59:16');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Product`
--

CREATE TABLE `Product` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `price` double NOT NULL,
  `category` varchar(255) NOT NULL,
  `stock` int(11) NOT NULL,
  `id_supplier` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `Product`
--

INSERT INTO `Product` (`id`, `name`, `description`, `price`, `category`, `stock`, `id_supplier`) VALUES
(3, 'Pan de mantequilla', 'Este es un pan mojado en mantequilla', 5000, 'Pan', 90, 1),
(10, 'string', 'string', 1, 'string', 2, 1),
(11, 'Producto de Prueba', 'Descripción del producto de prueba', 99.99, 'Electrónicos', 100, 1),
(12, 'Producto de Prueba', 'Descripción del producto de prueba', 99.99, 'Electrónicos', 100, 1),
(13, 'Producto Actualizado', 'Descripción actualizada', 150.5, 'Electrónicos', 75, 1),
(14, 'Producto de Prueba', 'Descripción del producto de prueba', 99.99, 'Electrónicos', 100, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Sale`
--

CREATE TABLE `Sale` (
  `id` int(10) UNSIGNED NOT NULL,
  `date` date NOT NULL,
  `id_client` int(10) UNSIGNED NOT NULL,
  `id_product` int(10) UNSIGNED NOT NULL,
  `Units` int(11) NOT NULL,
  `total` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `Sale`
--

INSERT INTO `Sale` (`id`, `date`, `id_client`, `id_product`, `Units`, `total`) VALUES
(1, '2025-06-18', 1, 3, 0, 10000),
(2, '2025-06-18', 1, 3, 10, 1000),
(3, '2025-06-18', 1, 3, 3, 15000),
(4, '2025-06-18', 1, 3, 15, 75000),
(5, '2025-06-18', 2, 3, 1000, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Supplier`
--

CREATE TABLE `Supplier` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) NOT NULL,
  `phone` int(11) NOT NULL,
  `address` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `Supplier`
--

INSERT INTO `Supplier` (`id`, `name`, `phone`, `address`) VALUES
(1, 'Alqueria', 21390102, 'Bogotá CL10');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `Client`
--
ALTER TABLE `Client`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `Complaint`
--
ALTER TABLE `Complaint`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Complaint_ibfk_1` (`idsale`);

--
-- Indices de la tabla `DeliveryStatus`
--
ALTER TABLE `DeliveryStatus`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idsale` (`idsale`);

--
-- Indices de la tabla `Employee`
--
ALTER TABLE `Employee`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `Inventory`
--
ALTER TABLE `Inventory`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_inventory_product` (`id_product`);

--
-- Indices de la tabla `Product`
--
ALTER TABLE `Product`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_supplier` (`id_supplier`);

--
-- Indices de la tabla `Sale`
--
ALTER TABLE `Sale`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `Supplier`
--
ALTER TABLE `Supplier`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `Client`
--
ALTER TABLE `Client`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `Complaint`
--
ALTER TABLE `Complaint`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `DeliveryStatus`
--
ALTER TABLE `DeliveryStatus`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `Employee`
--
ALTER TABLE `Employee`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `Inventory`
--
ALTER TABLE `Inventory`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `Product`
--
ALTER TABLE `Product`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `Sale`
--
ALTER TABLE `Sale`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `Supplier`
--
ALTER TABLE `Supplier`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `Complaint`
--
ALTER TABLE `Complaint`
  ADD CONSTRAINT `Complaint_ibfk_1` FOREIGN KEY (`idsale`) REFERENCES `Sale` (`id`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `DeliveryStatus`
--
ALTER TABLE `DeliveryStatus`
  ADD CONSTRAINT `DeliveryStatus_ibfk_1` FOREIGN KEY (`idsale`) REFERENCES `Sale` (`id`);

--
-- Filtros para la tabla `Inventory`
--
ALTER TABLE `Inventory`
  ADD CONSTRAINT `Inventory_ibfk_1` FOREIGN KEY (`id_product`) REFERENCES `Product` (`id`),
  ADD CONSTRAINT `fk_inventory_product` FOREIGN KEY (`id_product`) REFERENCES `Product` (`id`);

--
-- Filtros para la tabla `Product`
--
ALTER TABLE `Product`
  ADD CONSTRAINT `Product_ibfk_1` FOREIGN KEY (`id_supplier`) REFERENCES `Supplier` (`id`);

--
-- Filtros para la tabla `Sale`
--
ALTER TABLE `Sale`
  ADD CONSTRAINT `Sale_ibfk_1` FOREIGN KEY (`id_client`) REFERENCES `Client` (`id`),
  ADD CONSTRAINT `Sale_ibfk_2` FOREIGN KEY (`id_product`) REFERENCES `Product` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
