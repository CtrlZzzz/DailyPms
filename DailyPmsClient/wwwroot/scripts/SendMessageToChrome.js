export function SendToChrome() {
    window.chrome.runtime.sendMessage('kofiemopjlgoajfmkflhaijdfdocmhee', { message: 'Hello from Web app!' }, (response) => {
        console.log(response);
    })
}