USE [VideoRentalDB]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [UserName], [Password], [Address], [PhoneNumber], [Email], [Role]) VALUES (1, N'abc', N'123', N'HCM', N'0123456789', N'abc@gmai.com', N'Manager')
SET IDENTITY_INSERT [dbo].[Users] OFF
