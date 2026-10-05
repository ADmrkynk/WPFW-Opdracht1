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
        badge.appendChild(document.createTextNode("GitHub"));                                                                                                                                                   
                                                                                                                                                                                                                       
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
        const icon = document.createElement("img");
        icon.src = "images/github.svg";
        icon.alt = "";

        link.appendChild(icon);
        link.appendChild(document.createTextNode(" Bekijk profiel \u2192"));                                                                                                                                                           
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