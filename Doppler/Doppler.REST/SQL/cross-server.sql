
  EXEC sp_addlinkedserver @server = 'sql5103.site4now.net'

  EXEC sp_addlinkedsrvlogin 'sql5103.site4now.net'
                         ,'false'
                         ,NULL
                         ,''
                         ,''

  insert into [sql5103.site4now.net].[DB_A724E9_CeSium].[dbo].[Blobs] (Data) select Data from [DESKTOP-LHH5DA0].[DopplerDB].[dbo].[Blobs]

  select * from Blobs
  insert into Blobs (Data) values ((select * from OPENROWSET
(BULK N'', SINGLE_BLOB) as T1))
insert into [sql5103.site4now.net].[DB_A724E9_CeSium].[dbo].[Files] 
(Id, FileName, BLOBId) select Id, FileName, BLOBId from [DESKTOP-LHH5DA0].[DopplerDB].[dbo].[Files] where Id=3