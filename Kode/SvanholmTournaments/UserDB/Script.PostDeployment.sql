if not exists (select 1 from dbo.[Users])
begin
	insert into dbo.[Users] (FirstName, LastName, Achieved)
	values ('Admin', 'Admin', 0);
end