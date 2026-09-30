function renderLoading (container) {
    container.innerHTML = "";
    const p = document.createElement("p");
    p.textContent = "Github statistieken laden...";
    container.appendChild(p);  
}

function renderError (container, message) {
    container.innerHTML = "";
    const p = document.createElement("p");
    p.textContent = message;
    p.style.color = "#dc2626";
    container.appendChild(p);
}

function renderGitHubStats (container, userData) {
        container.innerHTML = "";                                                                                                                                                                                      
                                                                                                                                                                                                                       
        const widget = document.createElement("div");                                                                                                                                                                  
        widget.className = "github-widget";                                                                                                                                                                            
                                                                                                                                                                                                                       
        const badge = document.createElement("div");                                                                                                                                                                   
        badge.className = "github-status-badge";                                                                                                                                                                       
        const indicator = document.createElement("span");                                                                                                                                                              
        indicator.className = "live-indicator";                                                                                                                                                                        
        badge.appendChild(indicator);                                                                                                                                                                                  
        badge.appendChild(document.createTextNode("Live API Data"));                                                                                                                                                   
                                                                                                                                                                                                                       
        const pUser = document.createElement("p");                                                                                                                                                                     
        pUser.className = "github-username";                                                                                                                                                                           
        pUser.textContent = "@" + userData.login;                                                                                                                                                                      
                                                                                                                                                                                                                       
        const metrics = document.createElement("div");                                                                                                                                                                 
        metrics.className = "github-metrics";                                                                                                                                                                          
                                                                                                                                                                                                                       
        const box1 = document.createElement("div");                                                                                                                                                                    
        box1.className = "metric-box";                                                                                                                                                                                 
        const num1 = document.createElement("span");                                                                                                                                                                   
        num1.className = "metric-number";                                                                                                                                                                              
        num1.textContent = userData.public_repos;                                                                                                                                                                      
        const label1 = document.createElement("span");                                                                                                                                                                 
        label1.className = "metric-label";                                                                                                                                                                             
        label1.textContent = "Repositories";                                                                                                                                                                           
        box1.appendChild(num1);                                                                                                                                                                                        
        box1.appendChild(label1);                                                                                                                                                                                      
                                                                                                                                                                                                                       
        const box2 = document.createElement("div");                                                                                                                                                                    
        box2.className = "metric-box";                                                                                                                                                                                 
        const num2 = document.createElement("span");                                                                                                                                                                   
        num2.className = "metric-number";                                                                                                                                                                              
        num2.textContent = new Date(userData.created_at).getFullYear();                                                                                                                                                
        const label2 = document.createElement("span");                                                                                                                                                                 
        label2.className = "metric-label";                                                                                                                                                                             
        label2.textContent = "Lid sinds";                                                                                                                                                                              
        box2.appendChild(num2);                                                                                                                                                                                        
        box2.appendChild(label2);                                                                                                                                                                                      
                                                                                                                                                                                                                       
        metrics.appendChild(box1);                                                                                                                                                                                     
        metrics.appendChild(box2);                                                                                                                                                                                     
                                                                                                                                                                                                                       
        const pLink = document.createElement("p");                                                                                                                                                                     
        const link = document.createElement("a");                                                                                                                                                                      
        link.href = userData.html_url;                                                                                                                                                                                 
        link.target = "_blank";                                                                                                                                                                                        
        link.className = "github-btn";                                                                                                                                                                                 
        const svgIcon = `<svg width="16" height="16" viewBox="0 0 16 16" fill="currentColor" aria-hidden="true"><path d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1. 
  49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02. 
  08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .
  21.15.46.55.38A8.013 8.013 0 0016 8c0-4.42-3.58-8-8-8z"/></svg>`;                                                                                                                                                       
        link.innerHTML = `${svgIcon} Bekijk profiel \u2192`;                                                                                                                                                           
        pLink.appendChild(link);                                                                                                                                                                                       
                                                                                                                                                                                                                       
        widget.appendChild(badge);                                                                                                                                                                                     
        widget.appendChild(pUser);                                                                                                                                                                                     
        widget.appendChild(metrics);                                                                                                                                                                                   
        widget.appendChild(pLink);                                                                                                                                                                                     
                                                                                                                                                                                                                       
        container.appendChild(widget); 
}

async function fetchGitHubStats() {
    const container = document.getElementById("github-stats");
    if (!container) return;

    renderLoading(container);

    try {
        const response = await fetch("https://api.github.com/users/ADmrkynk")

        if (!response.ok) {
            throw new Error ("Kon gegevens niet ophalen van GitHub.");
        }

        const userData = await response.json();
        renderGitHubStats(container, userData);
    } catch (error) {
        renderError(container, "Er is een fout opgetreden bij het laden van GitHub.");
    }
    
}

document.addEventListener ("DOMContentLoaded", () => {
    fetchGitHubStats();
})