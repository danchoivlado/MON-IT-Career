/*
03.	Data Deletion
REMOVE from colonists, those which are not assigned to any journey.
*/
DELETE colonists
FROM colonists
LEFT OUTER JOIN travel_cards AS tc ON colonists.id = tc.colonist_id
WHERE tc.colonist_id IS NULL;