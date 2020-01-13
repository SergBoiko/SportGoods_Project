INSERT INTO Products(Name, Description, Category, Price)
VALUES
	('Nike Elite Basketball', 'The Nike Elite Competition 29.5" Basketball offers superior control and grip on indoor or outdoor courts. 
Durable composite leather cover with a rotationally wound butyl carcass for better shape retention and wide channels for better ball control. 
Great feel and bounce.', 'Accessories', 89.99),
	('adidas Ultraboost 20 Running Shoe', 'A new day. A new run. Make it your best. These high-performance shoes feature a foot-hugging knit upper. 
Stitched-in reinforcement is precisely placed to give you support in the places you need it most. The soft elastane heel delivers a more comfortable fit. 
Responsive cushioning returns energy to your stride with every footstrike for that I-could-run-forever feeling.', 'Footwear', 179.99),
	('Nike Sportswear Club Fleece Mens Jogger', 'A closet staple, the Nike Sportswear Club Fleece Joggers combine a classic look with the soft comfort of fleece for an elevated, everyday look that you can wear every day.',
'Apparel', 55.00),
	('McDavid Knee Brace with Polycentric Hinges', 'Bi-lateral geared polycentric hinges for stability and free movement
Engineered top and bottom straps assure personalized fit
Latex-free neoprene provides thermal compression
Open 360˚ padded buttress isolates and supports the patella
Bound edges prevent irritation
Perforated back panel provides heat and moisture management and all-around comfort
Fits left or righ', 'Accessories', 69.99),
	('Nike Pro 6 Inch Mens Compression Short', 'Whether its game day or training day, the Nike Pro Shorts help keep you supported and comfortable with a tight, stretchy fit and sweat-wicking technology. 
Layer them or wear them solo.', 'Apparel', 25.00),
	('Nike Dry Elite 1.5 Crew Basketball Socks', 'Lighter than its predecessor, Unisex Nike Dry Elite 1.5 Crew Basketball Sock is designed with a secure fit and ventilation in key areas so you can stay focused on your game.',
'Footwear', 14.00),
	('Trigger Point 12 Inch Core Mini Foam Roller', 'The TriggerPoint CORE™ Roller is the ideal foam roller for anyone that is new to foam rolling, prefers softer compression, or may be recovering from an injury. 
The high density EVA foam delivers moderate compression, for a comfortable total body massage. Its GRID pattern technology makes it the only solid foam roller on the market to channel blood and oxygen while you roll. 
The multi-density surface pattern is designed to break up knots and tight muscles, to aid in muscle recovery, improve flexibility, and enhance mobility.', 'Accessories', 19.99),
	('Nike Brasilia X-Large Training Backpack', 'The Nike Brasilia (Extra-Large) Training Backpack is designed for secure storage with a large compartment and small-item pockets inside and out. Its ultra-durable design features padded shoulder straps for comfort when you are on the go.',
'Accessories', 55.00),
	('Nike Air Monarch IV Mens Training Shoe', 'Mens Nike Air Monarch IV (4E) Training Shoe sets you up for comfortable training with durable leather on top for support. A lightweight foam midsole with a full-length encapsulated Air-Sole unit cushions every stride.', 'Footwear', 64.99);

	CREATE TABLE [dbo].[Products] (
    [ProductId]   INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (250)  NOT NULL,
    [Description] NVARCHAR (MAX)  NOT NULL,
    [Category]    NVARCHAR (100)  NOT NULL,
    [Price]       DECIMAL (16, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

DROP TABLE Products

