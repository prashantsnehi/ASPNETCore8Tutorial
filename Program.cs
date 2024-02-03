﻿var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // adds all the controller classes as services

var app = builder.Build();
app.MapControllers();

app.Run();

