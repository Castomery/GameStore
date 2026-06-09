export interface AuthResponse {
    token: string;
    userName: string;
    email: string;
}

export interface UserProfile {
    id : number;
    name: string;
    email: string;
}