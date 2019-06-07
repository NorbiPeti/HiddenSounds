---
title: FAQ
---
# Frequently Asked Questions
## What is Hidden Sounds?
Hidden Sounds is a group of your faviourite independent artists joining together to support 100% independent releases.

The Spotify playlist is updated weekly with new music and there are streams on Twitch every Tuesday and Thursday at 2 PM EST.

## Is it Thursday yet?
<span id="thursday">Maybe</span>
<script>
    const d = new Date().getDay();
    var text="";
    if(d == 1) text="No, but tomorrow's Tuesday!";
    else if(d == 2) text="No, but it's Tuesday!";
    else if(d == 3) text="Not yet, it's tomorrow!";
    else if(d == 4) text="Yes!";
    else text="No, but Tuesday is "+(d == 0 ? 9-d-7 : 9-d) //Hehe
    +" days away!";
    document.getElementById("thursday").innerText=text;
</script>
