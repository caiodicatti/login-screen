export interface authenticationResponse {
    success: boolean;
    statusCode: number;
    result: authentication;
    message: string;
}

export interface authentication {
    Email: string;
    Senha: string;
}