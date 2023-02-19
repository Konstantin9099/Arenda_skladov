CREATE DATABASE `Arenda_skladov` ;
USE `Arenda_skladov` ;

-- -----------------------------------------------------
-- Table `avtorizaciya`
-- -----------------------------------------------------
CREATE TABLE `avtorizaciya` (
  `id_avtorizaciya` INT NOT NULL AUTO_INCREMENT,
  `login` VARCHAR(20) NOT NULL,
  `password` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`id_avtorizaciya`));

-- -----------------------------------------------------
-- Table `sklady`
-- -----------------------------------------------------
CREATE TABLE `sklady` (
  `id_sklad` INT NOT NULL AUTO_INCREMENT,
  `adress_sk` VARCHAR(50) NULL,
  `nomer_sk` VARCHAR(2) NULL,
  `naznachenie_sk` VARCHAR(100) NULL,
  `ploshchad_sk` DECIMAL(6,2) NULL,
  `stoimost_ed` DECIMAL(6,2) NULL,
  `stoimost_poln` DECIMAL(6,2) NULL,
  PRIMARY KEY (`id_sklad`));


-- -----------------------------------------------------
-- Table `arendatory`
-- -----------------------------------------------------
CREATE TABLE `arendatory` (
  `id_arendator` INT NOT NULL AUTO_INCREMENT,
  `naimenovanie_ar` VARCHAR(100) NULL,
  `adress_ar` VARCHAR(100) NULL,
  `rukovoditel_ar` VARCHAR(50) NULL,
  `telefon_ar` VARCHAR(12) NULL,
  `email_ar` VARCHAR(45) NULL,
  PRIMARY KEY (`id_arendator`));


-- -----------------------------------------------------
-- Table `dogovory`
-- -----------------------------------------------------
CREATE TABLE `dogovory` (
  `id_dogovor` INT NOT NULL AUTO_INCREMENT,
  `nomer_dog` VARCHAR(10) NULL,
  `data1_dog` DATE NULL,
  `data2_dog` DATE NULL,
  `sklad_id` INT NULL,
  `arendstor_id` INT NULL,
  PRIMARY KEY (`id_dogovor`),
  CONSTRAINT `fk_dogovory_sklady`
    FOREIGN KEY (`sklad_id`)
    REFERENCES `sklady` (`id_sklad`),
  CONSTRAINT `fk_dogovory_arendatory`
    FOREIGN KEY (`arendstor_id`)
    REFERENCES `arendatory` (`id_arendator`));


-- -----------------------------------------------------
-- Table `nachisleniya`
-- -----------------------------------------------------
CREATE TABLE `nachisleniya` (
  `nachislenie_id` INT NOT NULL AUTO_INCREMENT,
  `dogovor_id` INT NULL,
  `data_nach` DATE NULL,
  `period_nach` VARCHAR(20) NULL,
  `summa_nach` DECIMAL(8,2) NULL,
  PRIMARY KEY (`nachislenie_id`),
  CONSTRAINT `fk_nachisleniya_dogovory`
    FOREIGN KEY (`dogovor_id`)
    REFERENCES `dogovory` (`id_dogovor`));


-- -----------------------------------------------------
-- Table `oplaty`
-- -----------------------------------------------------
CREATE TABLE `oplaty` (
  `id_oplata` INT NOT NULL AUTO_INCREMENT,
  `nachislenie_id` INT NULL,
  `arendator_id` INT NULL,
  `data_oplat` DATE NULL,
  `summa_opl` DECIMAL(8,2) NULL,
  PRIMARY KEY (`id_oplata`),
  CONSTRAINT `fk_oplaty_nachisleniya`
    FOREIGN KEY (`nachislenie_id`)
    REFERENCES `nachisleniya` (`nachislenie_id`),
  CONSTRAINT `fk_oplaty_arendatory`
    FOREIGN KEY (`arendator_id`)
    REFERENCES `arendatory` (`id_arendator`));



INSERT INTO `avtorizaciya` VALUES (1,'111','111');

INSERT INTO `arendatory` VALUES (1,'ООО Инновационные технологии','г. Мурманск, ул. Сталелетейная, д. 10, кв. 15','Петров Виктор Алексеевич','+74543121531','84543121531@mail.ru'),(2,'ОАО Зеленый мир','г. Омск, ул. Ленина, 135/1','Мамаева Алена Игоревна','+75631368465','85631368465@gmail.com'),(3,'ООО Виктория','г. Новосибирск, ул. Советская, 33/2','Карташев Евгений Витальевич','+72149653168','82149653168@mail.ru');

INSERT INTO `sklady` VALUES (1,'г. Новосибирск, ул. Промышленная, 5/1','1','Овощи и фрукты',100.00,900.00,90000.00),(2,'г. Новосибирск, ул. Промышленная, 5/1','2','Крупы, зернобобовые и макаронные изделия',80.00,700.00,56000.00),(3,'г. Новосибирск, ул. Промышленная, 5/2','3','Бытовая химия',80.00,800.00,64000.00),(4,'г. Новосибирск, ул. Промышленная, 5/2','4','Строительные материалы',300.00,800.00,240000.00),(5,'г. Новосибирск, ул. Промышленная, 5/3','5','Автозапчасти',150.00,800.00,12000.00),(6,'г. Новосибирск, ул. Промышленная, 5/3','6','Автошины',200.00,800.00,160000.00),(7,'г. Новосибирск, ул. Промышленная, 5/4','7','Кисломолочные продукты',50.00,800.00,40000.00);

INSERT INTO `dogovory` VALUES (1,'1','2023-02-01','2024-02-01',1,NULL),(2,'2','2023-02-02','2023-02-12',2,2),(3,'3','2023-02-10','2025-02-19',3,3);

INSERT INTO `nachisleniya` VALUES (1,NULL,'2023-02-19','за февраль 2023',90000.00),(2,2,'2023-02-19','за февраль 2023',56000.00),(3,1,'2023-02-19','за фераль 2023',64000.00);

INSERT INTO `oplaty` VALUES (1,1,1,90000.00,'2023-02-19'),(3,2,2,56000.00,'2023-02-19');


