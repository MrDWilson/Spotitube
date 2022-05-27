import os
from flask import Flask, redirect
from flask_session import Session
import requests
import json

app = Flask(__name__)
app.config['SECRET_KEY'] = os.urandom(64)
app.config['SESSION_TYPE'] = 'filesystem'
app.config['SESSION_FILE_DIR'] = './.flask_session/'
Session(app)

@app.route('/')
def token():
    return ""#requests.get("http://spotitube_token/").text

@app.route('/songs')
def songs():
    return requests.get("http://spotitube_songs/").text

@app.route('/song')
def song():
    return requests.post(url="http://spotitube_songs/song", data=json.dumps({"url": "3kEitzucVh9qoTG3CykSmo"}), headers = {'content-type': 'application/json'}).text

'''
Following lines allow application to be run more conveniently with
`python app.py` (Make sure you're using python3)
(Also includes directive to leverage pythons threading capacity.)
'''
if __name__ == '__main__':
    app.run(host='0.0.0.0')
