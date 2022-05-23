import http from 'k6/http';
import { check, group, sleep, fail } from 'k6';

export const options = {
  vus: 1, // 1 user looping for 1 minute
  duration: '1m',

  thresholds: {
    http_req_duration: ['p(99)<1500'], // 99% of requests must complete below 1.5s
  },
};

const BASE_URL = 'http://devops.adam-dev.eu:8091';
const USERNAME = 'test';
const PASSWORD = 'test123';

export default () => {
  const loginRes = http.post(`${BASE_URL}/api/Auth/Login`, {
    name: USERNAME,
    password: PASSWORD,
  });

  check(loginRes, {
    'http_req_duration': (resp) => resp.json('access') !== '',
  });

  const authHeaders = {
    headers: {
      Authorization: `Bearer ${loginRes.json('access')}`,
    },
  };

  const myObjects = http.get(`${BASE_URL}/api/Todo`, authHeaders).json();
  check(myObjects, { 'retrieved todos': (obj) => obj.length > 0 });

  sleep(1);
};
