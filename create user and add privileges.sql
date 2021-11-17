create user 'worksheet'@'localhost' ;

grant all PRIVILEGES ON worksheet.* TO 'worksheet'@'localhost' IDENTIFIED by 'nagggyontitkos';


select id, user, host, db, command, time, state, info, progress
from information_schema.processlist;