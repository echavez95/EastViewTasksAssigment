export const showAlert = (message, type) => {
    M.toast({ 
        html: message,
        classes: type === 'success' ? 'green' : 'red'
    });
}