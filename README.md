# studentpoll

A tool to get your students involved: display interactive questions, students answers them with their smartphones.

## Setup
You need:

- a webserver capable of running the frontend (php)
- mono on the same server for the backend
- no active process on port 1337 ;-)

Don't forget to import the sql file and edit the credentials in studentpoll-frontend/admin.php and the studentpoll-selfhost.exe.config!

## Adding Questions
Adding questions is done directly in MySQL. You need to prepare (a) a Poll in the `Polls` Table, and (b) Possible Answers in the `PollAnswers` Table.

## Usage
Open presentation.php on your local projector, and tell the students the URL of index.php to open (tip: use a QR Code). Students will see a white screen upon startup, this is normal :)

Open admin.php and paste the security token (currently hardcoded, but soon loaded from the config) in the box. You now have three options:

- Display: Displays the Question on the presentation screen.
- Activate: Puts the Question on the student's devices and lets them vote for 30 seconds (currently hardcoded).
- Results: Displays the Results on the presentation screen

Note that re-activating a question **does not** clear the Results, you have to manually execute `DELETE FROM PollVotes WHERE PollId = 1337`!

Pull requests & Issues are welcome.

License: MIT.
