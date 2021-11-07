START TRANSACTION;

INSERT INTO rentals.locators ("Id", "FirstName", "LastName", "BirthDate", "Street", "City", "Number", "District", "Cpf") VALUES 
('e4eafac2-0e6d-463d-b76a-cab2a9ff2f7c','Bob', 'Aerso', '1980-11-11', 'Rua Pinhanco', 'São Paulo', '467', 'São Paulo', '72414306033');

INSERT INTO rentals.librarians ("Id", "FirstName", "LastName", "BirthDate", "Street", "City", "Number", "District", "Cpf") VALUES 
('e2487e01-5cf7-43bc-b186-a64312e4bb49','John', 'Hiake', '1967-01-11', 'Parque Primavera', 'São Paulo', '338', 'São Paulo', '72414306033');


INSERT INTO rentals.books ("Id", "Title", "Author", "Status", "PhotoUrl") VALUES 
('44bf17e5-ffd2-4219-8211-28a37448a119','Fight Club', 'Chuck Palahniuk', 1, 'https://m.media-amazon.com/images/P/0393327345.01._SCLZZZZZZZ_SX500_.jpg'),
('bb58cd57-2148-4d46-a001-8cc4ce93ad4f','The Almanack of Naval Ravikant', 'Eric Jorgenson', 1, 'https://m.media-amazon.com/images/P/B08FF8MTM6.01._SCLZZZZZZZ_SX500_.jpg'),
('59ba2440-1f66-4ac7-8637-f68df6e1758d', 'Choke', 'Chuck Palahniuk', 1, 'https://m.media-amazon.com/images/P/0385720920.01._SCLZZZZZZZ_SX500_.jpg'),
('ef0e074f-2789-4635-8668-b6855d4b3973', 'Greenlights', 'Matthew McConaughey', 1, 'https://m.media-amazon.com/images/I/51DZeZw7K0L.jpg');

INSERT INTO inventory.books ("Id", "Title", "Author", "ReleasedYear", "Pages", "Version", "ISBN", "PhotoUrl") VALUES 
('44bf17e5-ffd2-4219-8211-28a37448a119','Fight Club', 'Chuck Palahniuk', '2005', '218', '1', '978-0393327342', 'https://m.media-amazon.com/images/P/0393327345.01._SCLZZZZZZZ_SX500_.jpg'),
('bb58cd57-2148-4d46-a001-8cc4ce93ad4f','The Almanack of Naval Ravikant', 'Eric Jorgenson', '2020', '244', '1', '000-0000000000', 'https://m.media-amazon.com/images/P/B08FF8MTM6.01._SCLZZZZZZZ_SX500_.jpg'),
('59ba2440-1f66-4ac7-8637-f68df6e1758d', 'Choke', 'Chuck Palahniuk', '2002', '304', '1', '978-0385720922', 'https://m.media-amazon.com/images/P/0385720920.01._SCLZZZZZZZ_SX500_.jpg'),
('ef0e074f-2789-4635-8668-b6855d4b3973', 'Greenlights', 'Matthew McConaughey', '2020', '304', '1', '978-0593139134', 'https://m.media-amazon.com/images/I/51DZeZw7K0L.jpg');

COMMIT;