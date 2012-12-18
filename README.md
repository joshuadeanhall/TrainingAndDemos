statuos
=======


Statuos is a project/task management system. 

Statuos was moved from tfs to github to make the code public and to move maintance of the server to another party (Also to learn about git(hub))

Development tasks are

-Fix current bugs

-Mark Task Complete

-Mark Project Complete

-Charge Hours to Project  (Will be ready for use at this point)

-Display graph showing charges over time for a task and for a project

-Improve UI with AJAX and general UI enchancements

-Add logging using interception and error handling with ELMAH


Statuos uses the following technologies.

ASP.NET MVC 4

EF Code First

AutoMapper

Castle Windsor


Statuos is still in early development and is not ready for testing/using.  Once the core features are done UI improvements will be performed.  
Currently the goal is to use an SVG package for displaying the graph, something like JQGrid for the grids, and converting a free css template to
a razor layout.

The SQL queries need to be examined for issues and the controllers need to be examined to ensure that there are no N+1 select statements.