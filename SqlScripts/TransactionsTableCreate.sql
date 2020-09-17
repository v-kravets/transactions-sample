CREATE TABLE [CurrencyTransaction] (
    Id varchar(50) Primary key,
    [Amount] decimal Not Null,
	[CurrencyId] tinyint Not Null,
	[TimestampUtc] Datetime Not Null,
	[StatusId] tinyint Not Null,
	FOREIGN KEY ([CurrencyId]) REFERENCES Currency([Id]),
	FOREIGN KEY ([StatusId]) REFERENCES [Status](Id)
);

CREATE INDEX DateTimeUtcIdx ON [CurrencyTransaction] ([TimestampUtc] desc);
CREATE INDEX StatusIdx ON [CurrencyTransaction] ([StatusId]);
CREATE INDEX CurrencyIdx ON [CurrencyTransaction] ([CurrencyId]);