1/9/2013

More UI improvments.  Most of the client area has been moved to using modal dialogs except for the detail views since the views are somewhat complex.  Even though the dialogs are loaded through ajax the saving is still doing a post back.  I would like for this to be done async but I don't know if I am willing to sacrifice the time to do this.

I have noticed that there is a lot of copy/paste for the js that I am using, I hope to refactor that into some helpers.

The js is currently in the pages, I plan to extract that out into js files soon and reference those.

I still got to clean up the admin pages then the site will have the ajax where it is needed.  I am currently leaning toward not doing server side paging.  In the future I hope to slowly add this in but I don't forsee anyone needing to work on 100+ customers, users, projects, tasks, or charges anytime in the near future.

Once I clean up the admin pages the last thing left to do is work on getting the graphs on the project and task site for task charges.  Once that is done I will do some final testing and fix any issues I discover.  I will then clean up the home page with information on the project and I may attempt to apply a free css template.  Once this is complete I will consider the project done.  From that point I will slowly be adding additional features (Mostly improve the ajax functionality) and add unit testing and bug fixes.  If anyone uses the project and finds bugs I will try to quickly fix those items.

1/7/2013

I am doing additional UI improvements on lists using DataTables and began thinking "How many of the possible features do I need to implement".  I could simply never implement paging/sorting(ajax)/filtering and be done much faster, or I could implement the features for every possible list and make sure I cover the functionality.  At this point I would normally go to the customer and ask but in this case I am the customer.  I don't like implementing unused features so I am only going to implement features where it will commonly be used.  I am also going to make the realistic use case and say that most(all) users of the system will likely be very small so I can expect only a few users assigned to a project/task and that a single project will have a resonable amount of charges to it, and will have a small amount of tasks associated with it.  With this assumption I can stick to paging and filtering being handled on the client side with the datatables library.  

The larger lists, all tasks, all projects, all users, all customers I can expect to be large enough to need paging and server side processing.

1/6/2013

Most of the functionality of the application has been developed so I am now turning my attention to the usability of the application.  I plan to redo all the lists with the jquery plug-in DataTables.  With this I will be improving the look and feel of the site.  I hope to remove a lot of the simple operations to modal dialogs and keep most operations from doing a post back.  

Once I am done with the lists and AJAX operations I will begin on the charts then will move to custom (free) css template for the site to improve it looks.  Once that is done I will begin testing, once complete the initial stage will be done and the project will be ready to used.  

12/27/2012

I am going to leave notes from this point forward describing my thoughts on the project.  Hopefully this will help people understand my thought process on why I did something.

At this point most of the base functionality is there.  An admin can do basic CRUD on customers, users, projects.  Users are never deleted but are instead marked inactivity and that is checked to ensure that a user is active throughout the system.  It is likely that the checks do not occur everywhere currently.  I hope to find and fix most of those later.

Currently there is no logging or exception handling anywhere in the code.  This is done mostly on purpose and partially because that code isn't much fun to write.  I will soon introduce ELMAH and logging via Interception(AOP) with castle windsor.  When I started the project I really did not want to have to concentrate on cross cutting issues like logging and decided I would use AOP to solve this issue, since castle windsor can accomplish this via interception it seems like a good fit.  While doing this project I learned about ELMAH and decided I wanted to test it out and see how it works during this project.  I hope to start implementing this functionality soon.


The UI is pretty bad currently.  I have some plans to improve the UI but my skills as a designer are extremely limited.  I will eventually find a free css template and apply that to the site and make some different layouts for the pages to better display the information.  The task assigned to users being an example of a div that could be moved to the side.  I will be fixing the Display names on the models to make the lists look better but I plan for most of the lists to become JavaScript grids.  I have not decided on a framework yet for the grid but I expect the grid to come long before the css template.  The current application uses far too many post backs so I plan to fix that with some AJAX and some jquery modal dialogs when I implement the JavaScript grids.  This should provide the users with a nice experience while using the site.


One of the last big features that I want to implement is charge graphs on the project details page and on the task details page.  I am looking at a few svg and canvas libraries that can help with this.  I am currently learning toward Raphaël (I really dislike the special e in the name as it forces me to have to copy and paste since I will never remember the keys to display it) since I have used svg in the past and would like to revisit it with a proper library helping me create graphs.

When initially designing the application I had to make a choice between using repositories and services or not.  I decided I would use repositories(actually I just created a generic repository which some may think is bad anyway), uow, and services.  Even with a simple application like this I find that the extra abstractions seem to hinder my efforts to solve problems.  In the future when using an ORM I will not bother with those abstractions (DBContext implements UoW and DbSets act like Aggregate Roots anyway).  I will also not be using a service layer and think I will try to implement commands into my solutions.  I really like the idea of directly allowing access to DbSets and then just separating complex queries out for reuse later.  


For the last 2 years, half of my career as a developer, I have been developing for SharePoint mostly by myself on small tasks.  Usually less than 1000 lines of code.  The projects usually make heavy use of the non-interface'd classes of SharePoint and are difficult and painful to unit test and usually involves moles.  Due to this and very short deadlines I have limited experience with testing.  I am trying to maintain about 60% code coverage currently and will continue to add unit tests later.  When I started with my unit tests I thought it would be a good idea to have a common setup for my mocks, I no longer feel like this was a smart thing to do and feel limited by it and find that my unit tests are now more confusing.  Over time I hope to eliminate this and do all the setup for each test at the start of the test.
