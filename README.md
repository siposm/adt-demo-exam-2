# Advanced Development Techniques Demo Exam #2

```
Sipos Miklos
2022
```

Create a **single-layer** console application based on the following tasks.

## Database creation
- Create a service-based database and name it `UserTweers`.
- Create the corresponding tables / entity classes using code-first approach like:
    - User
        - `int Id` - (auto-incremented primary key)
        - `string UserName` - the user's username
        - `string UserEmail` - the user's email address
    - Tweet
        - `int Id` - (auto-incremented primary key)
        - `string Content` - the tweet's text content
        - `bool Flagged` - tweet is flagged / marked for any reason or not
        - `int Year` - year of the tweet's creation
- For convenience override the `ToString` methods in both classes and display all the properties.
- Add data annotation to the `User` class so the created table name will be `Users`.
- Add data annotation to the `Tweet` class so the created table name will be `Tweets`.
- Add data annotation to the `User` class so the `UserEmail` property is required.
- Add navigation properties to both classes to create a **one-to-many** relation between the two. One user can have any number of tweets.
- Create the corresponding `UserTweetContext` class.
    - enable lazyloading
    - define the one-to-many relation between the two entities with the Fluent API in `OnModelCreating`
- Test the so far created functions from the Main method.

## XML processing
- Create a `UserTweetManager` class which will be responsible to read the XML file provided, process it and thus create entities. Image this as an API endpoint which is called whenever the program starts, so it fetches the latest records for us.
- Create a `XMLReader` named static method inside which will return `IEnumerable<User>` referenced collection.
- Inside this method, create a `Func` delegate which takes a `string` parameter (this will be the web URL for the XML file) and returns an `IEnumerable<User>` referenced collection.
- Process the xml inside this delegate and create the user and tweet entities, return them.
- Call the delegate from the method, and return the collection.

## Testing
- From the main method process the xml file from this URL: https://users.nik.uni-obuda.hu/siposm/db/user-tweets.xml
- Loop through the items and write out the user's and their tweets.

## Attribute usage
- Create a class named `ContainsCharacterAttribute`.
- Define a constructor in this class which takes a `string` parameter, as an array of characters. Create a property in the class, assign the constructor parameter to it.
- Add restriction so that this attribute can only be placed on properties.
- Apply the attribute to the `User`'s `UserEmail` property and add `@` and `.` characters.
    - So the aim will be to check via validation if the `UserEmail` contains all the given characters, both `@` symbol and `.` (at) symbol.

## Validation using reflection
- Create a class named `Validator`.
- Create inside a `UserEmailValidator` named static method which returns a `boolean` value and takes two parameters:
    - `string propertyName`
    - `T genericObject`
- Inside the method create a `Func` delegate which takes a `string` and a `T` parameters and returns a `boolean`.
- Inside this func delegate create an algorithm that can validate the given object by the given property and by the `ContainsCharacterAttribute` applied on it. Check which characters are available through the attribute and check if all of them is contained in the property itself.
    - `testemail@something.com` is a valid email address because it has both `@` and `.` characters
    - `testemail_something.com` is invalid because it is missing `@` character
    - `testemail@something_com` is invalid because it is missing `.` character
- Call the delegate from the method, and return the output.

## Testing
- Loop through the elements and validate all of them using the UserEmailValidator. If the user's email was valid, save it to the database; otherwise write out to the console that the user was not fulfilled the requiremenets.
- Loop through the items (queried from the database!) and write out the user's and their tweets.

## LINQ data querying
- In the `UserTweetManager` class create the following methods and queries.
- Remarks:
    - All methods should use the database as the source, so pass the `UserTweetContext` as parameter.
    - For this moment, all methods can return `IEnumerable<object>` referenced collection, but **this is not advised** in real projects! Always create Entity/DTO classes for these purposes as well and use them instead!
- Create the following queries:
    - `GetUsersWithHotmailAccount`
        - select the usernames of those who has hotmail email addresses. Convert their usernames to uppercase format.
    - `GetUsersWithAtLeastOneOldTweet`
        - select the users (without duplicate) who has at least one old tweet, meaning that the tweet was created before 2010.
    - `GetUsersWithTweetCount`
        - count how many tweets each user have and return them back in a anonymous class containing `UserName` and `TweetCount` properties.
    - `AverageTweetLengthByFlag`
        - calculate what is the average length of the tweets for the flagged ones and non-flagged ones (flagged status can be groupped). Use anonymous class with `IsFlagged` and `AverageLength` properties.
    - `SumOfTweetYearsByFlag`
        - sum the year of the tweets in the flagged and non-flagged groups. Use anonymous class with `IsFlagged` and `SumYears` properties.
    - `GetTweetNumberForMailType`
        - count how many tweets are created for each mail type. By mail type we mean the domain endings in the users' email like `@something.com` or `@hotmail.net`.
- Call the methods from the main method.