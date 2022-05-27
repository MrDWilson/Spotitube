import os
from xmlrpc.client import DateTime
from flask import Flask, redirect
from flask_session import Session
import spotipy
from datetime import datetime, timedelta
from typing import Optional

app = Flask(__name__)
app.config['SECRET_KEY'] = os.urandom(64)
app.config['SESSION_TYPE'] = 'filesystem'
app.config['SESSION_FILE_DIR'] = './.flask_session/'
Session(app)

code = os.environ['SPOTIFY_CODE']

auth_manager = spotipy.oauth2.SpotifyOAuth(scope='playlist-modify-public',
                                           show_dialog=True)

token: Optional[str] = None
expiry: Optional[DateTime] = None


def get_token():
    global token
    global expiry
    if token is not None and expiry is not None and expiry > datetime.now():
        return token

    token_info = auth_manager.get_access_token(code)
    token = token_info['access_token']
    expiry = datetime.now() + timedelta(seconds=int(token_info['expires_in']))
    return token


def generate_code():
    return auth_manager.get_authorize_url()


def process_code(response):
    return auth_manager.parse_response_code(response)

@app.route('/')
def request_token():
    return get_token()

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=80)
