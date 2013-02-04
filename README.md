directmodelbindingisbad
=======================
Just testing git

Simple project with binding models to views and then passing it on to to EF to persist the information.  This is a bad idea and unexpected things can occur.

This application can be tricked and a app user can edit a user and change the users money even though the money field doesn't appear, it is not even hidden anywhere on the form.  To do that simply modify the html and 

<input type="text"  name="Money" id="Money">

to the form and you can enter in the new Money and it will be persisted to the database.  This is clearly not something that you would want users to be able to do and is a good reason to use view models/DTOs/whatever instead of using objects that you persist to the database.

There are multiple ways to modify the html, I just used firebug and edited the html.
