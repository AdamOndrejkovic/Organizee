import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
  stages: [
    { duration: '2m', target: 400 }, // ramp up to 400 users
    { duration: '3h56m', target: 400 }, // stay at 400 for ~4 hours
    { duration: '2m', target: 0 }, // scale down. (optional)
  ],
};
const BASE_URL = 'http://devops.adam-dev.eu:8091';

export default function () {
  http.batch([
    ['GET', `${BASE_URL}/api/Todo`, null, { tags: { name: 'PublicCrocs' } }],
    ['GET', `${BASE_URL}/api/Todo`, null, { tags: { name: 'PublicCrocs' } }],
    ['GET', `${BASE_URL}/api/Todo`, null, { tags: { name: 'PublicCrocs' } }],
    ['GET', `${BASE_URL}/api/Todo`, null, { tags: { name: 'PublicCrocs' } }],
  ]);

  sleep(1);
}
