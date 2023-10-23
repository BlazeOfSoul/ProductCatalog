export interface ProductModelUserUpdate {
  id: string;
  name: string;
  description: string;
  priceInRubles: number;
  priceInDollars: number;
  generalNote: string;
  categoryName: string;
  editing?: boolean;
}
