As a user, I want to be able to create an account, so that I can ask questions and add responses.
As a user, I always want to see my login information at the top of the page with an option to log out.
As a user, I want to ask a question, so that I can get help with a programming problem I'm having.
As a user, I want to answer a question, so that I can help somebody else out.
As a user, I want to view a question and its responses, so that I can get help if I'm having the same problem.
As a user, I want the homepage to show all of the questions, most recent first, including number of votes and answers for each (see Stack Overflow's main page).
As a user who asked a question, I want to choose the best answer, so that it is displayed before the other answers and future users see it first.
As a user who didn't ask a question, I want to vote on which answer I think is best, so that higher-voted answers display higher on the page.
As a user, I want to vote up questions I like, so that awesome questions get displayed higher on the list of questions and on search results.

Classes:
Start by using this as reference: https://www.learnhowtoprogram.com/net/authentication-and-ajax/integrating-identity-with-entity
- Questions
    - question_id
    - title
    - content
    - public virtual ApplicationUser User {get;set;} = automatically generated user_id
    - datetime postdate
    - bool isClosed (isCloser = starts off active)
- Answers
    - answer_id
    - content
    - votes (people can click to vote for your answer)
    - datetime postdate
    - question_id
    - public virtual ApplicationUser User {get;set;} = automatically generated user_id
- Tags
- Comments

Code for allowing users to login with email/username: http://techbrij.com/asp-net-core-identity-login-email-username