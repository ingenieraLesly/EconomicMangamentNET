# EconomicMangamentNET 
donet C# Accounts
## Table Of Contents
1. [Commands Using](#comands)
2. [About Syntax](#syntax)
3. [Oder Programming](#steps)

<a name="comands"></a>
### 1. Commands Using 
***
Ctrl + K, Ctrl + C, --> comentar la selección   


Ctrl + K, Ctrl + D→para dar FORMATO  


F2 → sobre una carpeta para cambiarle el nombre  

<a name="syntax"></a>
### 2. About Syntax
***
NOMBRAR   


Variables: camelCase. (Ojo con palabras reservadas)  
Clases y Métodos: PascalCase  

<a name="steps"></a>
### 3. Order Programing
***
1. Create mvc (Agregar->Crear un Nuevo proyecto -->wirte mvc--> second in appear)
2. .csproj: Nullabble-> disable (to support null fields)
3. Create Controller
4. Create Model
5. Create View -> Create Folder -->Razor --> Name: Create.cshtml
6. DB Conection in appsettings.Development.json = 


 -with user and password in Managament Studio: 


 "Server= nameServer; Database=nameDB;**User ID=userLoginManagementS;Password=yourpassword**;Integrated Security=**false**" 


 -If you **don't** need a username and password to enter the Management Study --> (Trusted Connection): 


 Server=nameServer;Database=nameDB;Integrated Security=**True** 
